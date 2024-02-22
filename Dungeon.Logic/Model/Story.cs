using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [NotMapped]
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
        private set 
        {
            _currentRoom = value;
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
            return object.ReferenceEquals(CurrentRoom, Entrance);
        }
    }

    public bool EndOfGame
    {
        get
        {
            return CurrentRoom.EndOfGame;
        }
    }

    public Room Entrance { get; private set; }

    public void Begin()
    {
        CurrentRoom = Entrance;
    }

    public void Resume(IStoryRepository repo, int roomId)
    {
        CurrentRoom = repo.Navigate(roomId);
    }

    public void Navigate(IStoryRepository repo, string keyword)
    {
        Room exitRoom = CurrentRoom.Navigate(repo, keyword);
        if (exitRoom == null)
        {
            string availableExits = string.Join(", ", CurrentRoom.Exits.Select(exit => exit.Keyword));
            throw new NavigationException($"I do not understand what you mean by {keyword}, please read the story more carefully! Available exits: {availableExits}");
        }
        else
        {
            CurrentRoom = exitRoom;
        }
    }

}