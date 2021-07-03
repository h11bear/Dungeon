using System;
using System.IO;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Integration.Data
{
    public class StoryXmlRepositoryTests
    {
        [Fact]
        public void ReadsCatalogOfRooms()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            RoomCatalog catalog = repo.GetRooms(@"..\..\..\..\Dungeon.Logic\Story\MainDungeon.xml");
            catalog.Should().NotBeNull();
            catalog.Name.Should().Be("MainDungeon");
        }

        [Fact]
        public void ThrowsAnExceptionWhenXmlDoesNotExist()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            repo.Invoking(r => r.GetRooms(@"..\..\..\..\Dungeon.Logic\Story\BadName.xml"))
                .Should().Throw<FileNotFoundException>();
        }

        [Fact]
        public void FindRoomInCatalogAfterLoad()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            RoomCatalog catalog = repo.GetRooms(@"..\..\..\..\Dungeon.Logic\Story\MainDungeon.xml");
            Room doorRoom = catalog.Find("doorRoom");
            doorRoom.Should().NotBeNull();
            doorRoom.Narrative.Should().Contain("You open the door");
        }

        [Fact]
        public void NarrativeIsMissing()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            Action getRooms = () => repo.GetRooms(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\NarrativeBroken.xml");
            getRooms.Should().Throw<StoryDataException>()
                .And.Message.Should().Contain("Narrative is missing for the entrance room, please fix!");
        }

        [Fact]
        public void RoomNameIsMissing()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            Action getRooms = () => repo.GetRooms(@"..\..\..\..\Dungeon.Tests.Integration\Scenarios\RoomNameMissing.xml");
            getRooms.Should().Throw<StoryDataException>()
                .And.Message.Should().Contain("Room name is missing in RoomNameMissing, please review XML:");
        }
    }
}
