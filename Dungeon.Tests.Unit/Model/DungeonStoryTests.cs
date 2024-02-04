using System;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;

using System.Collections;
using System.Collections.Generic;
using Dungeon.Logic.Data;
using Moq;

namespace Dungeon.Tests.Unit.Model;
public class DungeonStoryTests
{
    private Mock<IStoryRepository> _mockRepo;
    public DungeonStoryTests()
    {
        _mockRepo = new Mock<IStoryRepository>();

    }

    [Fact]
    public void BeginTheStoryAtEntrance()
    {
        var entrance = new Room("entrance", "my entrance", null);

        Story story = new("test story", entrance, _mockRepo.Object);
        story.Begin();
        story.Narrative.Should().Be("my entrance");
    }

    [Fact]
    public void NavigateToTheMonsterRoom()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoom") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", new RoomExit[] { });

        Story story = new Story("test story", entrance, _mockRepo.Object);
        _mockRepo.Setup(mr => mr.FindRoom(story, "monsterRoom")).Returns(monsterRoom);

        story.Begin();
        story.Navigate("pursue monster");
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }

    [Fact]
    public void NavigateKeywordIsNotAnExit()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoom") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", new RoomExit[] { });

        Story story = new Story("test story", entrance, _mockRepo.Object);

        story.Begin();
        Action nav = () => story.Navigate("run");
        nav.Should().Throw<NavigationException>()
            .And.Message.Should().StartWith("I do not understand what you mean by run, please read the story more carefully!");
    }

    [Fact]
    public void ExitPointsToARoomThatDoesNotExist()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoomBadName") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", new RoomExit[] { });

        var story = new Story("test story", entrance, _mockRepo.Object);
        story.Begin();

        Action nav = () => story.Navigate("pursue monster");
        nav.Should().Throw<RoomNotFoundException>()
            .And.Message.Should().Be("Sorry, the programmer made a mistake!  Could not find room monsterRoomBadName!");
    }

    [Fact]
    public void GameEndsWhenRoomHasNoExits()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoom") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", new RoomExit[] { });

        var story = new Story("test story", entrance, _mockRepo.Object);
        _mockRepo.Setup(mr => mr.FindRoom(story, "monsterRoom")).Returns(monsterRoom);

        story.Begin();
        story.Navigate("pursue");
        story.EndOfGame.Should().BeTrue();
    }

    [Fact]
    public void GameEndsWhenRoomHasNullExits()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoom") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", null);

        var story = new Story("test story", entrance, _mockRepo.Object);
        _mockRepo.Setup(mr => mr.FindRoom(story, "monsterRoom")).Returns(monsterRoom);

        story.Begin();
        story.Navigate("pursue");
        story.EndOfGame.Should().BeTrue();
    }

    [Fact]
    public void ResumeGameAtRoom()
    {
        var exits = new RoomExit[] { new RoomExit("pursue", "monsterRoom") };
        var entrance = new Room("entrance", "my entrance, pursue the monster?", exits);
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?", new RoomExit[] { });

        var story = new Story("test story", entrance, _mockRepo.Object);
        _mockRepo.Setup(mr => mr.FindRoom(story, "monsterRoom")).Returns(monsterRoom);


        story.Resume("monsterRoom");
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }
}
