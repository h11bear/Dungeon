using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class NarrativeParser
    {


        public NarrativeParser()
        {

        }

        public List<StoryFragment> Parse(Room room) 
        {
            List<StoryFragment> fragments = new List<StoryFragment>();


            foreach(RoomExit exit in room.Exits) 
            {
                
                
            }

            return fragments;
        }

    }
}