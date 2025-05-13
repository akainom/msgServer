using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => { Console.WriteLine("\nGET"); return "Hello World!"; });

using var Server = new MessengerRepository();

app.MapPost("/auth/",  (AuthRequest request) => 
{
    Console.WriteLine($"Received username: {request.Username}");
    Console.WriteLine(Server.ExchangeKeys(request.Username, ECDiffieHellman.Create().ExportSubjectPublicKeyInfo()));
    return Server.AuthenticateUser(request.Username);

});

app.MapGet("/rsapublic/{username}", (string username) =>
{
    return Convert.ToBase64String(Server.GetUserRsaPublicKey(username).Modulus);
});

app.MapGet("/dhpublic/{username}", (string username) =>
{
    return Convert.ToBase64String(Server.GetUserDhPublicKey(username));
});

app.MapGet("/aespublic/{username}", (string username) =>
{
    return Convert.ToBase64String(Server.GetUserAesKey(username));
});

app.MapGet("/messages/{username}", (string username) =>
{
    return Server.GetUserMessages(username);
});

app.MapPost("/messages/send/",  (SendMessageRequest request) =>
{
    try
    {
        if (!Server.CheckIfUserExists(request.SenderUsername))
            return Results.NotFound($"User {request.SenderUsername} not found");

        if (!Server.CheckIfUserExists(request.ReceiverUsername))
            return Results.NotFound($"User {request.ReceiverUsername} not found");

        string decrypted;
        try
        {
            decrypted = Server.SendMessage(request.SenderUsername, request.ReceiverUsername, request.EncryptedMessage);
        }
        catch (AuthException ex)
        {
            return Results.Unauthorized();
        }

        byte[] reEncrypted = Server.ReceiveMessage(request.ReceiverUsername, decrypted);

        return Results.Ok(new MessageResponse(
            OriginalMessage: decrypted,
            ReEncrypted: reEncrypted,
            Timestamp: DateTime.UtcNow));
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: StatusCodes.Status500InternalServerError);
    }
});



app.Run();
