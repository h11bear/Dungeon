using System.IO;
using Dungeon.EntityFramework.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Dungeon.Tests.Integration.Data;

public class StoryContextSqlRepositoryTests
{

    public StoryContextSqlRepositoryTests()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        _configuration = builder.Build();
    }

    private IConfigurationRoot _configuration;

    [Fact]
    public void GetMainDungeonStory()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);

        var story = repo.GetStory("main");
        story.Name.Should().Be("main");
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
    public void EntranceNarrativeIsAvailableWhenLoadingStory()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);
        var story = repo.GetStory("main");
        story.Narrative.Should().Be("You find yourself in a dark room. You can open the door, explore, or follow the line of torches.");
    }

    [Fact]
    public void RoomIsLoadedWithExits()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);
        var story = repo.GetStory("main");
        story.CurrentRoom.Exits.Should().Contain(ex => ex.Keyword == "explore");
        story.CurrentRoom.Exits.Should().Contain(ex => ex.Keyword == "follow");
        story.CurrentRoom.Exits.Should().Contain(ex => ex.Keyword == "door");
    }

    [Fact]
    public void SuccessfullyNavigateToRoomExit()
    {
        var context = new DungeonContext(_configuration);

        StoryContextRepository repo = new(context);
        var story = repo.GetStory("main");
        story.Navigate(repo, "explore");

        story.CurrentRoom.Name.Should().Be("exploreRoom");
        story.CurrentRoom.Narrative.Should().Be("You look around the room and see a faint light glowing around the corner. There is also what looks like a bottomless pit across the room. What do you inspect first?");
    }

}