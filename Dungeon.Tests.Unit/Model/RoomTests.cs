using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;


namespace Dungeon.Tests.Unit.Model;
public class RoomTests
{
    [Fact]
    public void NavigateToARoomExit()
    {
        var myRoom = new Room("myRoom", "scary business");
        myRoom.WithExit("business", new Room("theBusinessRoom", "serious business in this room"));

        var businessExit = myRoom.Navigate("business");
        businessExit.Should().NotBeNull();
        businessExit.Name.Should().Be("theBusinessRoom");
    }

    [Fact]
    public void NavigateToARoomThatDoesNotExist()
    {
        var myRoom = new Room("myRoom", "scary business");
        myRoom.WithExit("business", new Room("theBusinessRoom", "serious business in this room"));

        var businessExit = myRoom.Navigate("bogus");
        businessExit.Should().BeNull();
    }

    [Fact]
    public void NavigateToARoomExitWhenOneKeywordMatches()
    {
        var myRoom = new Room("myRoom", "hey it is scary business");
        myRoom.WithExit("business", new Room("theBusinessRoom", "serious business in this room"));
        var businessExit = myRoom.Navigate("scary business");
        businessExit.Should().NotBeNull();
        businessExit.Name.Should().Be("theBusinessRoom");
    }

    [Fact]
    public void NavigateToExitWhenKeywordPartiallyMatches()
    {
        var myRoom = new Room("exploreRoom", "dance or follow the line of torches");
        myRoom.WithExit("dance", new Room("danceRoot", "some dancing narrative"));
        myRoom.WithExit("torch", new Room("torchRoom", "torch this place"));

        var torchRoomExit = myRoom.Navigate("follow the line of torches");
        torchRoomExit.Should().NotBeNull();
        torchRoomExit.Name.Should().Be("torchRoom");
    }

}
