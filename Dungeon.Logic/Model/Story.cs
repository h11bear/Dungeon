using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dungeon.Logic.Data;
namespace Dungeon.Logic.Model;

public class Story
{
    private Story()
    {

    }

    public Story(string name, Room entrance)
    {
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

    public void Resume(Room startingRoom)
    {
        _currentRoom = startingRoom;
    }

    public void Navigate(IStoryRepository repo, string keyword)
    {
        Room exitRoom = _currentRoom.Navigate(repo, keyword);
        if (exitRoom == null)
        {
            string availableExits = string.Join(", ", _currentRoom.Exits.Select(exit => exit.Keyword));
            throw new NavigationException($"I do not understand what you mean by {keyword}, please read the story more carefully! Available exits: {availableExits}");
        }
        else
        {
            _currentRoom = exitRoom;
        }
    }

}