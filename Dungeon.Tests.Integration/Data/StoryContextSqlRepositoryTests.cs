using System.IO;
using Dungeon.EntityFramework.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Dungeon.Tests.Integration.Data;

public class StoryContextSqlRepositoryTests {

    public StoryContextSqlRepositoryTests() {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        _configuration = builder.Build();
    }

    private IConfigurationRoot _configuration;

    [Fact]
    public void GetMainDungeonStory() {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);

        var story = repo.GetStory("main");
        story.Name.Should().Be("main");
        //story.Entrance.Should().NotBeNull();
        //story.CurrentRoom.Name.Should().Be("entrance");
    }

    [Fact]
    public void LoadingMainDungeonStoryIncludesTheEntrance()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);
        var story = repo.GetStory("main");
        story.Entrance.Should().NotBeNull();
    }

    [Fact]
    public void EntraceNarrativeIsAvailableWhenLoadingStory()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);
        var story = repo.GetStory("main");
        story.Narrative.Should().Be("You find yourself in a dark room. You can open the door, explore, or follow the line of torches.");
    }

}