# HospitalSystem

Hospital management web app built with ASP.NET Core MVC, Entity Framework Core and SQL Server.

## Features

- Manage doctors
- Book and manage patient appointments (patient name, date, time, doctor)
- Server-side validation with data annotations
- Entity Framework Core code-first migrations

## Tech stack

- .NET 10, ASP.NET Core MVC
- Entity Framework Core
- Microsoft SQL Server

## Getting started

1. Clone the repo: `git clone https://github.com/ahmedsaid114/task11.git`
2. Set `ConnectionStrings:DefaultConnection` in `HospitalSystem/appsettings.json` to your SQL Server instance.
3. Apply the database migrations:

   ```powershell
   dotnet ef database update
   ```

4. Run the project:

   ```powershell
   dotnet run --project HospitalSystem
   ```
