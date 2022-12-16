using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Dungeon.EntityFramework.Data;

namespace Dungeon.EntityFramework.Tests.Data;

public class RoomTests
{

    public RoomTests()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);
        
        _configuration = builder.Build();
    }

    private IConfigurationRoot _configuration;

    [Fact]
    public void GetDungeonEntrance()
    {
        var context = new DungeonContext(_configuration);

        var entrance = context.Rooms?.Single(room => room.Name.Equals("entrance"));

        entrance?.Narrative.Should().StartWith("You find yourself in a dark room.");
    }

    [Fact]
    public void GetExitsForTheDungeonEntrance()
    {
        var context = new DungeonContext(_configuration);

        var entrance = context.Rooms?.Single(room => room.Name.Equals("entrance"));

        entrance?.Exits.Count().Should().Be(3);
    }

}