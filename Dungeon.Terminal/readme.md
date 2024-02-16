## add reference to entity framework project

dotnet add reference ..\Dungeon.EntityFramework\Dungeon.EntityFramework.csproj


## seed Dungeon database with default story
./Dungeon.Terminal.exe seed ".\sqlexpress"

## Add appsettings support
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Hosting
