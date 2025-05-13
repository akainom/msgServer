using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
namespace DAL
{
    // Кастомные исключения
    public class AuthException : Exception
    {
        public AuthException(string message) : base($"Auth error: {message}") { }
    }

    public class KeyExchangeException : Exception
    {
        public KeyExchangeException(string message) : base($"Key exchange error: {message}") { }
    }

    public class MessageException : Exception
    {
        public MessageException(string message) : base($"Message error: {message}") { }
    }

    public interface IMessengerRepository : IDisposable
    {
        RSAParameters AuthenticateUser(string username);
        byte[] ExchangeKeys(string username, byte[] clientPublicKey);
        string SendMessage(string username, string reciever, byte[] encryptedMessage);
        byte[] ReceiveMessage(string username, string message);
        int SaveChanges();
    }

    public class MessengerRepository : IMessengerRepository
    {
        private Dictionary<string, RSAParameters> _userRsaPrivateKeys = new();
        private Dictionary<string, RSAParameters> _userRsaPublicKeys = new();
        private Dictionary<string, ECDiffieHellman> _userDhInstances = new();
        private Dictionary<string, byte[]> _userDhPublicKeys = new();
        private Dictionary<string, byte[]> _userAesKeys = new();
        private List<Message> _userMessages = new();
        private List<string> AuthenticatedUsers = new List<string>();
        public RSAParameters GetUserRsaPublicKey(string username)
        {
            if (!_userRsaPublicKeys.ContainsKey(username))
                throw new AuthException("User not authenticated");
            return _userRsaPublicKeys[username];
        }

        public byte[] GetUserDhPublicKey(string username)
        {
            if (!_userDhPublicKeys.ContainsKey(username))
                throw new KeyExchangeException("Key exchange not performed");
            return _userDhPublicKeys[username];
        }

        public byte[] GetUserAesKey(string username)
        {
            if (!_userAesKeys.ContainsKey(username))
                throw new KeyExchangeException("AES key not generated");
            return _userAesKeys[username];
        }

        public RSAParameters AuthenticateUser(string username)
        {
            if (AuthenticatedUsers.Contains(username)) { return _userRsaPublicKeys[username]; }
            using var rsa = RSA.Create();
            var privateKey = rsa.ExportParameters(true);
            var publicKey = rsa.ExportParameters(false);

            _userRsaPrivateKeys[username] = privateKey;
            _userRsaPublicKeys[username] = publicKey;

            AuthenticatedUsers.Add(username);
            return publicKey;
        }

        public byte[] ExchangeKeys(string username, byte[] clientPublicKey)
        {
            var dh = ECDiffieHellman.Create();
            byte[] serverPublicKey = dh.PublicKey.ExportSubjectPublicKeyInfo();

            // Сохраняем DH инстанс и публичный ключ
            _userDhInstances[username] = dh;
            _userDhPublicKeys[username] = serverPublicKey;

            // Вычисляем общий секрет
            using var clientDh = ECDiffieHellman.Create();
            clientDh.ImportSubjectPublicKeyInfo(clientPublicKey, out _);
            var sharedSecret = dh.DeriveKeyMaterial(clientDh.PublicKey);

            // Сохраняем AES ключ (первые 32 байта для AES-256)
            _userAesKeys[username] = sharedSecret[..32];

            return serverPublicKey;
        }

        // Остальные методы остаются без изменений

        public byte[] ReceiveMessage(string username, string message)
        {
            if (!_userAesKeys.TryGetValue(username, out var aesKey))
                throw new AuthException("User not authenticated or key not exchanged");

            return AesEncrypt(message, aesKey);
        }

        public int SaveChanges() => 1;

        private byte[] AesEncrypt(string text, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, 16);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
                writer.Write(text);

            return ms.ToArray();
        }

        private string AesDecrypt(byte[] encryptedData, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = encryptedData[..16];

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(encryptedData[16..]);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }

        public bool CheckIfUserExists(string username) => AuthenticatedUsers.Contains(username);

        public void SaveMessage(string sender, string recipient, string message)
        {
            _userMessages.Add(new Message(sender, recipient, message, DateTime.Now));
        }

        public string SendMessage(string sender, string reciever, byte[] encryptedMessage)
        {
            if (!_userAesKeys.TryGetValue(sender, out var aesKey))
                throw new AuthException("User not authenticated");

            string decrypted = AesDecrypt(encryptedMessage, aesKey);
            SaveMessage(sender, reciever, decrypted);
            return decrypted;
        }

        // Метод для получения сообщений
        public List<Message> GetUserMessages(string username)
        {
            return _userMessages
                .Where(m => m.Receiver == username)
                .ToList();
        }

        public void Dispose()
        {
            foreach (var dh in _userDhInstances.Values)
            {
                dh.Dispose();
            }
            _userDhInstances.Clear();
        }
    }
    public record AuthRequest(string Username);

    public class HTTPClient
    {
        public HttpClient client = new HttpClient();
        public string username;
        public string JSONHandler;
        public string url;
        public List<string> ClientKeys = new List<string>();
        public HTTPClient(string _username)
        {
            username = _username;
            var launchsettingsLocation = "C:\\Users\\kavop\\OneDrive\\Документы\\Belstu\\msgServer\\msgServer\\Properties\\launchSettings.json";
            var launchSettingsJSON = JsonNode.Parse(File.ReadAllText(launchsettingsLocation));
            JSONHandler = "Initialized";
            string? applicationUrl = launchSettingsJSON?["profiles"]?["http"]?["applicationUrl"].ToString();
            url = applicationUrl;
        }
        public async Task<HttpResponseMessage> Connect()
        {
            var launchsettingsLocation = "C:\\Users\\kavop\\OneDrive\\Документы\\Belstu\\msgServer\\msgServer\\Properties\\launchSettings.json";
            var launchSettingsJSON = JsonNode.Parse(File.ReadAllText(launchsettingsLocation));
            string? applicationUrl = launchSettingsJSON?["profiles"]?["http"]?["applicationUrl"].ToString();
            return await client.GetAsync(applicationUrl + "/");
        }
        public async Task<HttpResponseMessage> POSTAuthenticateUser(string username)
        {
            var json = JsonSerializer.Serialize(new { username });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(url + "/auth/", content);
        }
        public async Task<string> AnalyseResponse(HttpResponseMessage responseMessage)
        {
            responseMessage = responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content.ReadAsStringAsync().Result;
        }
        public async Task<List<Task<string>>> GetKeysForUser()
        {
            var RSAKeys = AnalyseResponse(await client.GetAsync(url + "/rsapublic/" + username));
            var DHKeys = AnalyseResponse(await client.GetAsync(url + "/dhpublic/" + username));
            var AESKeys = AnalyseResponse(await client.GetAsync(url + "/aespublic/" + username));
            List<Task<string>> Keys = new List<Task<string>> { RSAKeys, DHKeys, AESKeys };
            foreach (var item in Keys)
            {
                ClientKeys.Add(item.Result);
            }
            return Keys;
        }

        public async Task<MessageResponse> SendMessage(string destination, string msg)
        {
            var encrypted = EncryptMessage(msg, Convert.FromBase64String(ClientKeys[2]));
            var request = new SendMessageRequest(username, destination, encrypted);
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url + "/messages/send/", content);
            var result = await response.Content.ReadFromJsonAsync<MessageResponse>();
            return result;
        }

        public byte[] EncryptMessage(string text, byte[] aesKey)
        {
            using var aes = Aes.Create();
            aes.Key = aesKey;
            aes.GenerateIV(); // Важно: новый IV для каждого сообщения

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();

            // Записываем IV (первые 16 байт)
            ms.Write(aes.IV, 0, 16);

            // Шифруем тело сообщения
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs, Encoding.UTF8))
            {
                writer.Write(text);
            }

            return ms.ToArray();
        }
        public async Task<List<Message>> GetMessagesForUser()
        {
            var response = await client.GetAsync(url + $"/messages/{username}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Message>>()
                    ?? new List<Message>();
            }

            throw new HttpRequestException($"Error: {response.StatusCode}");
        }
    }
    public record SendMessageRequest(
    string SenderUsername,
    string ReceiverUsername,
    byte[] EncryptedMessage);

    public record Message(
    string Sender,
    string Receiver,
    string Content,
    DateTime Timestamp);

    public record MessageResponse(
        string OriginalMessage,
        byte[] ReEncrypted,
        DateTime Timestamp);

}