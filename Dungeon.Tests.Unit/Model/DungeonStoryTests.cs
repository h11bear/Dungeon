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
        entrance.WithExit("pursue", new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

        Story story = new Story("test story", entrance);

        story.Begin();
        story.Navigate("pursue monster");
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }

    [Fact]
    public void NavigateKeywordIsNotAnExit()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        entrance.WithExit("pursue", new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

        Story story = new Story("test story", entrance);

        story.Begin();
        Action nav = () => story.Navigate("run");
        nav.Should().Throw<NavigationException>()
            .And.Message.Should().StartWith("I do not understand what you mean by run, please read the story more carefully!");
    }

    [Fact]
    public void GameEndsWhenRoomHasNoExits()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        entrance.WithExit("pursue", new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

        var story = new Story("test story", entrance);

        story.Begin();
        story.Navigate("pursue");
        story.EndOfGame.Should().BeTrue();
    }

    [Fact]
    public void ResumeGameAtRoom()
    {
        var entrance = new Room("entrance", "my entrance, pursue the monster?");
        var monsterRoom = new Room("monsterRoom", "a scary monster is in front of you, run or fight?");

        var story = new Story("test story", entrance);
        story.Resume(monsterRoom);
        
        story.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
    }
}
