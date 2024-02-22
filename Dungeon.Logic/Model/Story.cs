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
        Entrance = entrance;
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
            if (_currentRoom == null)
            {
                _currentRoom = Entrance;
            }
            return _currentRoom;
        }
    }

    public string Narrative
    {
        get
        {
            return CurrentRoom.Narrative;
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