using System;
using Dungeon.Logic.Input;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class Room
    {

        public Room(string name, string narrative)
        {
            Name = name;
            Narrative = narrative;
        }
        public string Narrative { get; }
        public string Name { get; }
        public bool EndOfGame
        {
            get
            {
                return Exits.Count.Equals(0);
            }
        }

        public void AddExit(RoomExit roomExit) 
        {
            Exits.Add(roomExit);
        }

        public List<RoomExit> Exits { get; } = new List<RoomExit>();

        public void Enter(DungeonIo dungeonIo)
        {
            dungeonIo.WriteLine();
            dungeonIo.WriteLine(Narrative);
            dungeonIo.WriteLine();
        }

        public RoomExit Navigate(string phrase)
        {
            string[] words = phrase.Split(' ');

            foreach(RoomExit roomExit in Exits)
            {
                foreach(string word in words) 
                {
                    if (word.Contains(roomExit.Keyword, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return roomExit;
                    }

                }
            }

            return null;
        }

    }
}