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
    Mock<IStoryRepository> _mockRepo;
    public DungeonStoryTests()
    {
        _mockRepo = new();
    }

    [Fact]
    public void BeginTheStoryAtEntrance()
    {
        var entrance = new Room("entrance", "my entrance");

        Story story = new("test story", entrance);
        story.Begin();
        story.Narrative.Should().Be("my entrance");
    }

    [Fact]
    public void NavigateToTheMonsterRoom()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?");
        entrance.WithExit("pursue", monsterRoom);

        _mockRepo.Setup(mr => mr.Navigate(It.IsAny<int>())).Returns(monsterRoom);

        Story story = new Story("test story", entrance);

        story.Begin();
        story.Navigate(_mockRepo.Object, "pursue monster");
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }

    [Fact]
    public void NavigateKeywordIsNotAnExit()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        entrance.WithExit("pursue", new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

        Story story = new Story("test story", entrance);

        story.Begin();
        Action nav = () => story.Navigate(_mockRepo.Object, "run");
        nav.Should().Throw<NavigationException>()
            .And.Message.Should().StartWith("I do not understand what you mean by run, please read the story more carefully!");
    }

    [Fact]
    public void GameEndsWhenRoomHasNoExits()
    {
        
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?");
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        entrance.WithExit("pursue", monsterRoom);

        _mockRepo.Setup(mr => mr.Navigate(It.IsAny<int>())).Returns(monsterRoom);

        var story = new Story("test story", entrance);

        story.Begin();
        story.Navigate(_mockRepo.Object, "pursue");
        story.EndOfGame.Should().BeTrue();
    }

    [Fact]
    public void ResumeGameAtRoom()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?");

        var story = new Story("test story", entrance);
        _mockRepo.Setup(mr => mr.Navigate(5)).Returns(monsterRoom);
        story.Resume(_mockRepo.Object, 5);
        
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }
}
