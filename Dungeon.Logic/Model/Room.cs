using System;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class Room
    {

        public Room(int roomId, string name, string narrative)
        {
            RoomId = roomId;
            Name = name;
            Narrative = narrative;
        }
        public int RoomId {get; private set;}
        public string Narrative { get; private set;}
        public string Name { get; private set;}
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