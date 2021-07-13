using System;
using System.Linq;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class NarrativeParser
    {


        public NarrativeParser()
        {

        }

        public List<NarrativeFragment> Parse(Room room)
        {
            List<Tuple<int, string>> tokenPositions = new List<Tuple<int, string>>();

            foreach (RoomExit exit in room.Exits)
            {
                int tokenIndex = room.Narrative.IndexOf(exit.Keyword, System.StringComparison.CurrentCultureIgnoreCase);

                if (tokenIndex >= 0)
                {
                    tokenPositions.Add(new Tuple<int, string>(tokenIndex, exit.Keyword));
                }
            }

            List<NarrativeFragment> fragments = new List<NarrativeFragment>();
            string remainingNarrative = room.Narrative;
            foreach(var tokenPosition in tokenPositions.OrderBy((sp) => sp.Item1).ToArray()) 
            {
                int matchIndex = remainingNarrative.IndexOf(tokenPosition.Item2, System.StringComparison.CurrentCultureIgnoreCase);
                
                if (matchIndex != -1) 
                {
                    if (matchIndex > 0) 
                    {
                        fragments.Add(new NarrativeFragment(remainingNarrative.Substring(0, matchIndex), false));
                    }

                    fragments.Add(new NarrativeFragment(remainingNarrative.Substring(matchIndex, tokenPosition.Item2.Length), true));
                    
                    remainingNarrative = remainingNarrative.Substring(matchIndex + tokenPosition.Item2.Length);
                }
            }

            if (remainingNarrative.Length > 0) 
            {
                fragments.Add(new NarrativeFragment(remainingNarrative, false));
            }

            return fragments;
        }

    }
}