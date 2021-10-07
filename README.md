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

## Setup web project publishing
cd .\Dungeon.Web
dotnet publish -c Release -o ./publish

## Setting up Chromebook for development
https://tecadmin.net/how-to-install-dotnet-core-on-debian-10/  

## Markdown reference
https://www.markdownguide.org/basic-syntax/


## .deploymnet file
https://github.com/projectkudu/kudu/wiki/Customizing-deployments#deploying-a-specific-aspnet-or-aspnet-core-project-file

## Deployment options
https://wakeupandcode.com/deploying-asp-net-core-3-1-to-azure-app-service/

https://docs.microsoft.com/en-us/azure/app-service/deploy-local-git?tabs=cli

https://github.com/projectkudu/kudu/wiki/Deployment-branch

https://docs.microsoft.com/en-us/powershell/module/az.websites/publish-azwebapp?view=azps-6.2.1

## authentication
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio

## Migrate from .NET Core 3.1 to 5.0
https://docs.microsoft.com/en-us/aspnet/core/migration/31-to-50?view=aspnetcore-5.0&tabs=visual-studio-code

## To update NuGet packages versions run dotnet add again
dotnet add package <packagename>
