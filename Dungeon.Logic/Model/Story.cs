using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model;

public class Story
{
    private Story()
    {

    }

    public Story(string name, RoomCatalog roomCatalog, Room entrance)
    {
        this.Name = name;
        this.RoomCatalog = roomCatalog;
        this.Entrance = entrance;
        this._currentRoom = entrance;
    }

    public int StoryId { get; private set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; private set; }
    public RoomCatalog RoomCatalog { get; private set; }
    public Room Entrance { get; private set; }

    private Room _currentRoom;

    public string Narrative
    {
        get
        {
            return _currentRoom.Narrative;
        }
    }

    public bool IsEntrance
    {
        get
        {
            return object.ReferenceEquals(_currentRoom, Entrance);
        }
    }

    public bool EndOfGame
    {
        get
        {
            return _currentRoom.EndOfGame;
        }
    }


    public void Begin()
    {
        _currentRoom = Entrance;
    }

    public void Resume(string roomName)
    {
        EnterRoom(roomName);
    }

    public void Navigate(string keyword)
    {
        RoomExit roomExit = _currentRoom.Navigate(keyword);
        if (roomExit == null)
        {
            throw new NavigationException($"I do not understand what you mean by {keyword}, please read the story more carefully!");
        }
        else
        {
            EnterRoom(roomExit.RoomName);
        }
    }

    private void EnterRoom(string roomName)
    {
        _currentRoom = RoomCatalog.Find(roomName);

        if (_currentRoom == null)
        {
            throw new RoomNotFoundException($"Sorry, the programmer made a mistake!  Could not find room {roomName}!");
        }
    }



}