using System.Collections.Generic;
using Dungeon.Logic.Model;

namespace Dungeon.Web.Models
{
    public class DungeonStoryViewModel
    {
        public DungeonStoryViewModel(DungeonStory story)
        {
            var parser = new NarrativeParser();
            StoryFragments = parser.Parse(story.CurrentRoom);
            RoomName = story.CurrentRoom.Name;
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

        public string RoomName { get; }
        public List<NarrativeFragment> StoryFragments { get; }


    }
}
