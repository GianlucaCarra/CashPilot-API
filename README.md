<h1 align="center" id="title">CashPilot</h1>

<p id="description">This app is an income/expenses manager, where you can generate reports for granular control of your expenses.<br><br>You can use this app if you want, but it's not a project funded and only a personal project to showcase my C# skills.</p>



<h2>🧐 Features</h2>

Here are some of the project's best features:

*   Login with JWT
*   Account activation
*   Password reset/forgotten
*   PDF Report generation

<h2>🛠️ Installation Steps:</h2>

<p>1. You'll need the .NET SDK 9.0.306 or greater. If you don't have follow the steps to install in <a href="https://dotnet.microsoft.com/en-us/download">.NET Site</a>:</p>

```
dotnet --version
```

<p>2. Clone this repository (or download the .zip file and open):</p>

```
git clone https://github.com/GianlucaCarra/CashPilot-API 
cd CashPilot
```

<p>3. Restore project dependencies:</p>

```
dotnet restore
```

<p>4. Create a file called appsettings.Development.json and paste the appsettings.json filling the fields with yours connection strings</p>

<p>5. Run the project migrations:</p>

```
cd src 
dotnet ef database update --project CashPilot.Infrastructure
```

<p>6. Run the project:</p>

```
dotnet run --project CashPilot.API
```

<p>7. To run in debug mode with hot reload enabled run this:</p>

```
dotnet watch run --project CashPilot.API 
```



<h2>💻 Built with</h2>

Technologies used in the project:

*   EF Core
*   C#
*   SMTP
*   JWT
*   Redis
*   OAuth 2.0
*   SQLite
*   PostgresSQL
*   AutoMapper
*   FluentValidation
*   QuestPDF