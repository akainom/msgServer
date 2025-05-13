во первых удалить пакет Microsoft.AspNetCore.Mvc из msgServer.csproj

во вторых настроить DAL.HTTPClient.HTTPClient() конструктор, указав путь к своему файлу launchSettings.json : msgServer\msgServer\Properties\launchSettings.json

в третьих при использование : собрать решение(Ctrl+Shift+B), запустить проект msgServer, запустить клиентское приложение.
