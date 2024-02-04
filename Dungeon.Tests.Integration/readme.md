## upgrade nuget packages
dotnet add package FluentAssertions
dotnet add package xunit
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector


## add reference to entity framework

dotnet add reference ..\Dungeon.EntityFramework\Dungeon.EntityFramework.csproj


## Add appsettings support
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Hosting
