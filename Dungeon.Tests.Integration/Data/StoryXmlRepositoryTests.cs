using System;
using System.IO;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Integration.Data;
public class StoryXmlRepositoryTests
{
    [Fact]
    public void GetMainDungeonStory()
    {
        StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Logic\Story\MainDungeon.xml");

        Story story = repo.GetStory("main");

        story.Entrance.Name.Should().Be("entrance");
    }

    [Fact]
    public void ThrowsAnExceptionWhenXmlDoesNotExist()
    {
        Action initRepo = () =>
        {
            StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Logic\Story\BadName.xml");
        };

        initRepo.Should().Throw<FileNotFoundException>();
    }

    //[Fact]
    public void FindInnerRoomInRepo()
    {
        StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\RoomWithExits.xml");
        Story story = repo.GetStory("main");
        repo.FindRoom(story, "rockRoom").Should().NotBeNull();
    }

    [Fact]
    public void NarrativeIsMissing()
    {
        StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\NarrativeBroken.xml");

        Action initRepo = () =>
        {
            Story story = repo.GetStory("main");
        };

        initRepo.Should().Throw<StoryDataException>()
            .And.Message.Should().Contain("narrative is missing for the entrance");
    }

    [Fact]
    public void RoomNameIsMissing()
    {
        StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\RoomNameMissing.xml");

        Action initRepo = () =>
        {
            Story story = repo.GetStory("main");
        };

        initRepo.Should().Throw<StoryDataException>()
            .And.Message.Should().Contain("name attribute is missing for");
    }

    [Fact]
    public void LoadRoomWithBrokenExits()
    {
        StoryXmlRepository repo = new StoryXmlRepository(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\RoomWithBrokenExits.xml");
        Action initRepo = () =>
        {
            Story story = repo.GetStory("main");
        };

        initRepo.Should().Throw<StoryDataException>()
            .And.Message.Should().Contain("keyword attribute is missing for the exploreRoom exits");
    }

}

