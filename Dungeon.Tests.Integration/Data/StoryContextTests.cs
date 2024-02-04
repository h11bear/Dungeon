using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Dungeon.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Dungeon.EntityFramework.Tests.Data;

public class StoryTests
{
    public StoryTests()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        _configuration = builder.Build();
    }

    private IConfigurationRoot _configuration;

    [Fact]
    public void GetMainDungeon()
    {
        var context = new DungeonContext(_configuration);

        // https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager

        var story = context?.Stories?.Single(story => story.Name.Equals("main"));

        story?.Should().NotBeNull();
    }

    [Fact]
    public void MainDungeonHasAnEntrance()
    {
        var context = new DungeonContext(_configuration);

        // https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager

        var story = context?.Stories?.Single(story => story.Name.Equals("main"));

        story?.Entrance.Should().NotBeNull();
    }

    [Fact]
    public void GetExitsForTheDungeonEntrance()
    {
        var context = new DungeonContext(_configuration);

        var entrance = context.Rooms?.Single(room => room.Name.Equals("entrance"));

        entrance?.Exits.Count().Should().Be(3);
    }

}