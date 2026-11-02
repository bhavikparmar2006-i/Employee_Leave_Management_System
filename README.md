# Employee Leave Management System

A beginner-friendly ASP.NET Core MVC project for the BCA Full Stack Web Development course (DSC-M-BCA-471P). It uses Razor Views, C#, ASP.NET Core Identity, EF Core, SQL Server and REST API controllers.

## Requirements
- .NET 10 SDK
- SQL Server Express or SQL Server LocalDB
- VS Code with C# Dev Kit (or Visual Studio)

## Configure SQL Server
The default connection string in `appsettings.json` targets Windows LocalDB:

```json
"ConnectionStrings": { "DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=EmployeeLeaveManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" }
```

If using SQL Server Express, update it to your instance, for example `Server=localhost\SQLEXPRESS;Database=EmployeeLeaveManagementDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`. Do not commit real credentials.

## First-time setup
Open a terminal in this folder and run:

```bash
dotnet --info
dotnet tool install --global dotnet-ef
dotnet restore
dotnet ef database update
dotnet run
```

If `dotnet-ef` is already installed, run `dotnet tool update --global dotnet-ef` instead. The project already includes the `InitialCreate` migration. Then open the URL shown in the terminal.

## Development admin account
Set these environment variables before running the app. Use a strong password and change it after first login:

PowerShell:
```powershell
$env:SeedAdmin__Email="admin@example.com"
$env:SeedAdmin__Password="ChangeThis!234"
dotnet run
```

The app creates the Admin and Employee roles. It creates the configured admin account if both values are set. Do not use example credentials in production. When the Admin creates an employee, the application creates an Employee login and displays a temporary password once on the Employees page. Share it privately with the employee. After logging in, the employee can choose **Change password** in the navigation and enter their current password and a new password. An email already used by an existing login account cannot be reused for a new employee account.

## Run in VS Code
1. Extract the project ZIP.
2. Open the extracted folder in VS Code.
3. Configure the SQL Server connection string.
4. Run the first-time setup commands above.
5. Run `dotnet run` and open the displayed local URL.
6. For API testing in Development, open `/swagger` after starting the app. Endpoints require authentication; use an authenticated session or configure an API auth flow before testing protected endpoints.

## Main modules
- Identity login/logout and Admin/Employee roles.
- Admin dashboard with live counts from the database.
- Employee CRUD and search.
- Leave application, history, cancellation, approval/rejection, and date checks.
- Protected REST API for employee and leave records.
- Employee self-service password change through ASP.NET Core Identity.
- Responsive plain CSS layout.

## Important implementation notes
- The project includes the `InitialCreate` EF Core migration. For a fresh database, run `dotnet ef database update` before starting the app for the first time.
- Live build/database testing must be performed in an environment with the .NET 10 SDK and SQL Server. This workspace may not have those tools installed.
- Employee accounts are created by the Admin from the Employees page. Password changes require the employee’s current password and are processed by ASP.NET Core Identity. Admin seeding is supported through environment variables.
