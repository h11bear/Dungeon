using System;
using System.IO;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Unit.Model
{
    public class RoomCatalogTests
    {
        [Fact]
        public void FirstRoomIsTheEntrance()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            catalog.AddRoom(new Room("testName", "my test narrative"));
            Room entrance = catalog.GetEntrance();
            entrance.Should().NotBeNull();
            entrance.Name.Should().Be("testName");
        }

        [Fact]
        public void FindRoomInCatalog()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            catalog.AddRoom(new Room("testName", "my test narrative"));
            catalog.AddRoom(new Room("otherRoom", "my other narrative"));
            
            Room otherRoom = catalog.Find("otherRoom");
            otherRoom.Should().NotBeNull();
            otherRoom.Narrative.Should().Be("my other narrative");
        }

        [Fact]
        public void DoNotFindRoomInCatalog()
        {
            RoomCatalog catalog = new RoomCatalog("test catalog");
            catalog.AddRoom(new Room("testName", "my test narrative"));
            catalog.AddRoom(new Room("otherRoom", "my other narrative"));
            
            Room otherRoom = catalog.Find("otherRoom1");
            otherRoom.Should().BeNull();
        }

    }
}
