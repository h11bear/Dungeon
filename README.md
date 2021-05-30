# Dungeon
https://tecadmin.net/how-to-install-dotnet-core-on-debian-10/


https://docs.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code

Run the console terminal app:
dotnet run --project Dungeon.Terminal/Dungeon.Terminal.csproj

Run the web app:
dotnet run --project Dungeon.Web/Dungeon.Web.csproj


Create empty solution:
dotnet new sln

Create library project:
dotnet new classlib --framework netcoreapp3.1 -o Dungeon.Logic
dotnet sln add Dungeon.Logic/Dungeon.Logic.csproject

Create console project with reference to Dungeon.Logic:
dotnet new console --framework netcoreapp3.1 -o Dungeon.Terminal
dotnet sln add Dungeon.Terminal/Dungeon.Terminal.csproj
dotnet add Dungeon.Terminal/Dungeon.Terminal.csproj reference Dungeon.Logic/Dungeon.Logic.csproj

Create MVC web project with reference to Dungeon.Logic:
dotnet new mvc --framework netcoreapp3.1 --output Dungeon.Web
dotnet sln add Dungeon.Web/Dungeon.Web.csproj
dotnet add Dungeon.Web/Dungeon.Web.csproj reference Dungeon.Logic/Dungeon.Logic.csproj


