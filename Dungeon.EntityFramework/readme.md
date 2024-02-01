## Add SQL Server entity framework
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

## Update entity framework tools to latest version
dotnet tool update --global dotnet-ef

## Add appsettings support
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Hosting


## Add tests to infrastructure project
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector

## dependency injection in console app
https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage


## setup EF migrations and deploy database
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet ef migrations add RoomCatalogRename

## list and revert migrations
dotnet ef migrations list
dotnet ef database update 20221130103153_initialCreate

then you can remove migrations
dotnet ef migrations remove

## upgrade nuget packages
dotnet add package coverlet.collector
dotnet add package FluentAssertions
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
