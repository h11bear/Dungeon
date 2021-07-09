using System.Collections.Generic;
using Dungeon.Logic.Model;

namespace Dungeon.Web.Models
{
    public class DungeonStoryViewModel
    {
        public DungeonStoryViewModel(DungeonStory story)
        {

        }

        public List<StoryFragment> StoryFragments { get; } = new List<StoryFragment>();


    }
}
