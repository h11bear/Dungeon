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
            foreach (var tokenPosition in tokenPositions.OrderBy((sp) => sp.Item1).ToArray())
            {
                int matchIndex = remainingNarrative.IndexOf(tokenPosition.Item2, System.StringComparison.CurrentCultureIgnoreCase);

                if (matchIndex != -1)
                {
                    if (matchIndex > 0)
                    {
                        fragments.Add(NarrativeFragment.PlainText(remainingNarrative.Substring(0, matchIndex)));
                    }

                    //extend tokens to the end of word
                    int tokenLength = tokenPosition.Item2.Length;
                    int extendLength = 0;
                    for (int i = matchIndex + tokenLength; i < remainingNarrative.Length; i++)
                    {
                        if (Char.IsLetter(remainingNarrative[i]))
                        {
                            extendLength++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    string extendedText = remainingNarrative.Substring(matchIndex, tokenLength + extendLength);
                    fragments.Add(NarrativeFragment.NavLink(extendedText, tokenPosition.Item2));
                    remainingNarrative = remainingNarrative.Substring(matchIndex + extendedText.Length);
                }
            }

            if (remainingNarrative.Length > 0)
            {
                fragments.Add(NarrativeFragment.PlainText(remainingNarrative));
            }

            return fragments;
        }

    }
}