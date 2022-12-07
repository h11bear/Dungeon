using System;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Unit.Model
{
    public class DungeonStoryTests
    {
        [Fact]
        public void BeginTheStoryAtEntrance()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            catalog.AddRoom(new Room("entrance", "my entrance"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Begin();
            dungeonStory.Narrative.Should().Be("my entrance");
        }

        [Fact]
        public void NavigateToTheMonsterRoom()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            catalog.AddRoom(entrance);

            catalog.AddRoom(new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Begin();
            dungeonStory.Navigate("pursue monster");
            dungeonStory.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
        }

        [Fact]
        public void NavigateKeywordIsNotAnExit()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            catalog.AddRoom(entrance);

            catalog.AddRoom(new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Begin();
            Action nav = () => dungeonStory.Navigate("run");
            nav.Should().Throw<NavigationException>()
                .And.Message.Should().Be("I do not understand what you mean by run, please read the story more carefully!");
        }

        [Fact]
        public void ExitPointsToARoomThatDoesNotExist()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoomBadName"));
            catalog.AddRoom(entrance);

            catalog.AddRoom(new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Begin();
            Action nav = () => dungeonStory.Navigate("pursue monster");

            nav.Should().Throw<RoomNotFoundException>()
                .And.Message.Should().Be("Sorry, the programmer made a mistake!  Could not find room monsterRoomBadName!");

        }

        [Fact] 
        public void GameEndsWhenRoomHasNoExits()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            catalog.AddRoom(entrance);

            catalog.AddRoom(new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Begin();
            dungeonStory.Navigate("pursue");
            dungeonStory.EndOfGame.Should().BeTrue();
        }

        [Fact]
        public void ResumeGameAtRoom()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoomBadName"));
            catalog.AddRoom(entrance);

            catalog.AddRoom(new Room("monsterRoom", "a scary monster is in front of you, run or fight?"));

            DungeonStoryLegacy dungeonStory = new DungeonStoryLegacy(catalog);
            dungeonStory.Resume("monsterRoom");
            dungeonStory.Narrative.Should().Be("a scary monster is in front of you, run or fight?");
        }
    }
}
