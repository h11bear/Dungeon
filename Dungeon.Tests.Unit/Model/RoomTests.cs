using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;
using Moq;
using Dungeon.Logic.Data;


namespace Dungeon.Tests.Unit.Model;
public class RoomTests
{
    private Mock<IStoryRepository> _mockRepo;
    public RoomTests()
    {
        _mockRepo = new Mock<IStoryRepository>();
    }

    [Fact]
    public void NavigateToARoomExit()
    {
        var myRoom = new Room("myRoom", "scary business");
        var businessRoom = new Room("theBusinessRoom", "serious business in this room");
        myRoom.WithExit("business", businessRoom);

        _mockRepo.Setup(mr => mr.Navigate(It.IsAny<int>())).Returns(businessRoom);
        
        var businessExit = myRoom.Navigate(_mockRepo.Object, "business");
        businessExit.Should().NotBeNull();
        businessExit.Name.Should().Be("theBusinessRoom");
    }

    [Fact]
    public void NavigateToARoomThatDoesNotExist()
    {
        var myRoom = new Room("myRoom", "scary business");
        myRoom.WithExit("business", new Room("theBusinessRoom", "serious business in this room"));

        var businessExit = myRoom.Navigate(_mockRepo.Object, "bogus");
        businessExit.Should().BeNull();
    }

    [Fact]
    public void NavigateToARoomExitWhenOneKeywordMatches()
    {
        var myRoom = new Room("myRoom", "hey it is scary business");
        var businessRoom = new Room("theBusinessRoom", "serious business in this room");
        myRoom.WithExit("business", businessRoom);
        
        _mockRepo.Setup(mr => mr.Navigate(It.IsAny<int>())).Returns(businessRoom);

        var businessExit = myRoom.Navigate(_mockRepo.Object, "scary business");
        businessExit.Should().NotBeNull();
        businessExit.Name.Should().Be("theBusinessRoom");
    }

    [Fact]
    public void NavigateToExitWhenKeywordPartiallyMatches()
    {
        var myRoom = new Room("exploreRoom", "dance or follow the line of torches");
        var danceRoom = new Room("danceRooM", "some dancing narrative");
        var torchRoom = new Room("torchRoom", "torch this place");

        myRoom.WithExit("dance", danceRoom);
        myRoom.WithExit("torch", torchRoom);

        _mockRepo.Setup(mr => mr.Navigate(It.IsAny<int>())).Returns(torchRoom);

        var torchRoomExit = myRoom.Navigate(_mockRepo.Object, "follow the line of torches");
        torchRoomExit.Should().NotBeNull();
        torchRoomExit.Name.Should().Be("torchRoom");
    }

}
