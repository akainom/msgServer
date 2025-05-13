using DAL;
try
{
    // 1. Инициализация клиентов
    var client1 = new HTTPClient("test_user");
    var client2 = new HTTPClient("test_user2");
    
    // 2. Аутентификация
    await client1.Connect();
    await client1.POSTAuthenticateUser(client1.username);
    await client1.GetKeysForUser();

    await client2.Connect();
    await client2.POSTAuthenticateUser(client2.username);
    var keys = await client2.GetKeysForUser();

    // 3. Отправка тестового сообщения
    string testMsg = "Hello from user1!";
    var sendResponse = await client1.SendMessage(client2.username, testMsg);
    Console.WriteLine($"Message sent: {sendResponse.OriginalMessage}");
    await client1.SendMessage(client2.username, "privet");
    await client1.SendMessage(client2.username, "priv");
    await client1.SendMessage(client2.username, "hello");
    await client1.SendMessage(client2.username, "nihao");
    // 4. Получение сообщений
    await Task.Delay(200); // Небольшая задержка для обработки

    var messages = await client2.GetMessagesForUser();
    Console.WriteLine($"Messages for {client2.username}:");
    foreach (var msg in messages)
    {
        Console.WriteLine($"[{msg.Timestamp}] From {msg.Sender}: {msg.Content}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Test failed: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
    }
}