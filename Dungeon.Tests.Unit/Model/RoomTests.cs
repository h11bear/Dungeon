using System;
using System.IO;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Unit.Model
{
    public class RoomTests
    {
        [Fact]
        public void NavigateToARoomExit()
        {
            Room myRoom = new Room("myRoom", "scary business");
            myRoom.AddExit(new RoomExit("business", "theBusinessRoom"));
            
            RoomExit businessExit = myRoom.Navigate("business");
            businessExit.Should().NotBeNull();
            businessExit.RoomName.Should().Be("theBusinessRoom");
        }

        [Fact]
        public void NavigateToARoomThatDoesNotExist()
        {
            Room myRoom = new Room("myRoom", "scary business");
            myRoom.AddExit(new RoomExit("business", "theBusinessRoom"));
            
            RoomExit businessExit = myRoom.Navigate("bogus");
            businessExit.Should().BeNull();
        }

        [Fact]
        public void NavigateToARoomExitWhenOneKeywordMatches()
        {
            Room myRoom = new Room("myRoom", "hey it is scary business");
            myRoom.AddExit(new RoomExit("business", "theBusinessRoom"));
            
            RoomExit businessExit = myRoom.Navigate("scary business");
            businessExit.Should().NotBeNull();
            businessExit.RoomName.Should().Be("theBusinessRoom");
        }

        [Fact]
        public void NavigateToExitWhenKeywordPartiallyMatches()
        {
            Room myRoom = new Room("exploreRoom", "dance or follow the line of torches");
            myRoom.AddExit(new RoomExit("dance", "danceRoom"));
            myRoom.AddExit(new RoomExit("torch", "torchRoom"));
            
            RoomExit torchRoomExit = myRoom.Navigate("follow the line of torches");
            torchRoomExit.Should().NotBeNull();
            torchRoomExit.RoomName.Should().Be("torchRoom");
        }

    }
}
