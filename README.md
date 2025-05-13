во первых удалить пакет Microsoft.AspNetCore.Mvc из msgServer.csproj
во вторых настроить DAL.HTTPClient.HTTPClient() конструктор, указав путь к своему файлу launchSettings.json : msgServer\msgServer\Properties\launchSettings.json
