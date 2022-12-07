using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Dungeon.EntityFramework.Tests.Common;

public class AppSettingsTests
{


    [Fact]
    public void GetDatabaseConnection()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);
        
        var configuration = builder.Build();

        configuration.GetValue<string>("ConnectionStrings:DungeonDbConnectionString").Should().Be("Data Source=.\\sqlexpress;Initial Catalog=Dungeon;TrustServerCertificate=True;Trusted_Connection=True");
    }
}