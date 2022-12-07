using System.Collections.Generic;
using Dungeon.Logic.Model;

namespace Dungeon.RazorPages.Models
{
    public class DungeonStoryViewModel
    {
        public DungeonStoryViewModel(DungeonStoryLegacy story)
        {
            var parser = new NarrativeParser();
            Story = story;
            StoryFragments = parser.Parse(story.CurrentRoom);
            if (story.IsEntrance)
            {
                Headline = "Welcome to the Dungeon!";
            }
            else
            {
                Headline = "You're getting into the depths....";
            }
        }

        public string Headline { get; set; }

        public string RoomName
        {
            get
            {
                return Story.CurrentRoom.Name;
            }
        }
        public DungeonStoryLegacy Story { get; }
        public List<NarrativeFragment> StoryFragments { get; }

        public bool EndOfGame
        {
            get
            {
                return Story.EndOfGame;
            }
        }


    }
}
