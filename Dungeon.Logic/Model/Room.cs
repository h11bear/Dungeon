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

        public RoomExit Navigate(string choice)
        {
            foreach(RoomExit roomExit in Exits)
            {
                if (roomExit.Keyword.Equals(choice, StringComparison.CurrentCultureIgnoreCase))
                {
                    return roomExit;
                }
            }

            return null;
        }

    }
}