using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model;

public class Story
{
    private Story()
    {

    }

    public Story(string name, Room entrance)
    {
        this.Name = name;
        this.Entrance = entrance;
    }
    
    public int StoryId { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; }
    public Room Entrance { get; private set; }


}