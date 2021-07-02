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
            RoomCatalog catalog = repo.GetRooms(@"..\..\..\..\Dungeon.Logic\Story\Entrance.xml");
            catalog.Should().NotBeNull();
            catalog.Name.Should().Be("Entrance");
        }

        [Fact]
        public void ThrowsAnExceptionWhenXmlDoesNotExist()
        {
            StoryXmlRepository repo = new StoryXmlRepository();
            repo.Invoking(r => r.GetRooms(@"..\..\..\..\Dungeon.Logic\Story\BadName.xml"))
                .Should().Throw<FileNotFoundException>();
        }
    }
}
