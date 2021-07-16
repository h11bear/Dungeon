# Dungeon

## Run ASP.NET MVC web app
dotnet run --project Dungeon.Web/Dungeon.Web.csproj

## Run ASP.NET MVC web app in watch mode
dotnet watch run --project Dungeon.Web/Dungeon.Web.csproj

## Run console app
dotnet run --project Dungeon.Terminal/Dungeon.Terminal.csproj

## Run units tests in watch mode
create Dungeon.csproj file in root solution to recursively watch files, example towards bottom of this link:  
https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch?view=aspnetcore-5.0

dotnet watch test

## Run integration tests
cd .\Dungeon.Tests.Integration\
dotnet test

## Create empty solution
dotnet new sln

## Create library project
dotnet new classlib --framework netcoreapp3.1 -o Dungeon.Logic  
dotnet sln add Dungeon.Logic/Dungeon.Logic.csproject

## Create console project with reference to Dungeon.Logic
dotnet new console --framework netcoreapp3.1 -o Dungeon.Terminal  
dotnet sln add Dungeon.Terminal/Dungeon.Terminal.csproj  
dotnet add Dungeon.Terminal/Dungeon.Terminal.csproj reference Dungeon.Logic/Dungeon.Logic.csproj

## Create MVC web project with reference to Dungeon.Logic
dotnet new mvc --framework netcoreapp3.1 --output Dungeon.Web  
dotnet sln add Dungeon.Web/Dungeon.Web.csproj  
dotnet add Dungeon.Web/Dungeon.Web.csproj reference Dungeon.Logic/Dungeon.Logic.csproj

## Create unit test project
dotnet new xunit --framework netcoreapp3.1 --output Dungeon.Tests.Unit  
dotnet sln add Dungeon.Tests.Unit/Dungeon.Tests.Unit.csproj

## Add fluent assertions package
cd Dungeon.Tests.Unit  
dotnet add package FluentAssertions

## Create integration test project
dotnet new xunit --framework netcoreapp3.1 --output Dungeon.Tests.Integration  
dotnet sln add Dungeon.Tests.Integration/Dungeon.Tests.Integration.csproj  
dotnet add Dungeon.Tests.Integration/Dungeon.Tests.Integration.csproj reference Dungeon.Logic/Dungeon.Logic.csproj

## Setting up Chromebook for development
https://tecadmin.net/how-to-install-dotnet-core-on-debian-10/  
https://docs.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code

## Mardown reference
https://www.markdownguide.org/basic-syntax/
