## Microsoft Get Started
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio

https://www.learnrazorpages.com/

## User secrets
%APPDATA%\Microsoft\UserSecrets
dotnet user-secrets set "AppSettings:DatabasePassword" "<password>"

## add PostgreSQL entity framework support, remove Sqlite
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet remove package Microsoft.EntityFrameworkCore.Sqlite

## deploy Identity to PostgreSQL
https://decovar.dev/blog/2020/10/17/dotnet-core-identity-postgresql/
dotnet ef database update 0
dotnet ef migrations remove
dotnet ef migrations add CreateIdentitySchema
dotnet ef database update

## add pages
dotnet new page --name Mistake --namespace Dungeon.RazorPages.Pages --output Pages
dotnet new page --name NavigationError --namespace Dungeon.RazorPages.Pages --output Pages

## run app in watch mode
dotnet watch run