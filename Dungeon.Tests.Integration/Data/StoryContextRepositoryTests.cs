using System.IO;
using Dungeon.EntityFramework.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Dungeon.Tests.Integration.Data;

public class StoryContextRepositoryTests {

    public StoryContextRepositoryTests() {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        _configuration = builder.Build();
    }

    private IConfigurationRoot _configuration;

    // [Fact]
    public void GetMainDungeonStory() {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);

        var story = repo.GetStory("main");
        story.Entrance.Should().NotBeNull();

        //story.CurrentRoom.Name.Should().Be("entrance");
    }

}