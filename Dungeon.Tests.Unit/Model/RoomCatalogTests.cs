using System;
using System.IO;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Unit.Model;
public class RoomCatalogTests
{

    [Fact]
    public void FindRoomInCatalog()
    {
        var rooms = new Room[] {new Room("testName", "my test narrative", null),
            new Room("otherRoom", "my other narrative", null)
            };
        var catalog = new RoomCatalog("test catalog", rooms);

        var otherRoom = catalog.Find("otherRoom");
        otherRoom.Should().NotBeNull();
        otherRoom.Narrative.Should().Be("my other narrative");
    }

    [Fact]
    public void DoNotFindRoomInCatalog()
    {
        var rooms = new Room[] {new Room("testName", "my test narrative", null),
            new Room("otherRoom", "my other narrative", null)
            };
        var catalog = new RoomCatalog("test catalog", rooms);

        var otherRoom = catalog.Find("otherRoom1");
        otherRoom.Should().BeNull();
    }

}
