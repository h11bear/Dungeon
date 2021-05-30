# Dungeon
https://tecadmin.net/how-to-install-dotnet-core-on-debian-10/


https://docs.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code

Run the console terminal app:
dotnet run --project .\Dungeon.Terminal\Dungeon.Terminal.csproj

Create empty solution:
dotnet new sln

Create library project:
dotnet new classlib -o Dungeon.Logic
dotnet sln add Dungeon.Logic/Dungeon.Logic.csproject

Create console project:
dotnet new console -o Dungeon.Terminal
dotnet sln add Dungeon.Terminal/Dungeon.Terminal.csproj

Console project add a reference to Dungeon.Logic
dotnet add Dungeon.Terminal/Dungeon.Terminal.csproj reference Dungeon.Logic/Dungeon.Logic.csproj

