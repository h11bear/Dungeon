using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dungeon.Logic.Data;
namespace Dungeon.Logic.Model;

public class Story
{
    private IStoryRepository _storyRepository;
    private Story()
    {

    }

    public Story(string name, Room entrance, IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
        Name = name;
        //this._rooms.AddRange(rooms);
        Entrance = entrance;
        _currentRoom = entrance;
    }

    public int StoryId { get; private set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; private set; }
    private Room _currentRoom;
    public Room CurrentRoom
    {
        get
        {
            return _currentRoom;
        }
    }

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

    //private List<Room> _rooms = new List<Room>();
    //public IEnumerable<Room> Rooms => _rooms;

    public Room Entrance { get; private set; }

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
            string availableExits = string.Join(", ", _currentRoom.Exits.Select(exit => exit.RoomName));
            throw new NavigationException($"I do not understand what you mean by {keyword}, please read the story more carefully! Available exits: {availableExits}");
        }
        else
        {
            EnterRoom(roomExit.RoomName);
        }
    }

    private void EnterRoom(string roomName)
    {
        _currentRoom = Find(roomName);

        if (_currentRoom == null)
        {
            throw new RoomNotFoundException($"Sorry, the programmer made a mistake!  Could not find room {roomName}!");
        }
    }

    private Room Find(string roomName) 
    {
        return _storyRepository.FindRoom(this, roomName);
    }


}