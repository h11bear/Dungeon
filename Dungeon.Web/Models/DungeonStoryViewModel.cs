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
        }

        public string RoomName { get; }
        public List<NarrativeFragment> StoryFragments { get; }


    }
}
