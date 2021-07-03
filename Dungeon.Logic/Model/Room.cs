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
                return exits.Count.Equals(0);
            }
        }

        public void AddExit(string choice, Room exitRoom)
        {
            exits[choice.ToLower()] = exitRoom;
        }

        public void AddExit(RoomExit roomExit) 
        {
            Exits.Add(roomExit);
        }

        public List<RoomExit> Exits { get; } = new List<RoomExit>();

        private Dictionary<string, Room> exits = new Dictionary<string, Room>();

        public void Enter(DungeonIo dungeonIo)
        {
            dungeonIo.WriteLine();
            dungeonIo.WriteLine(Narrative);
            dungeonIo.WriteLine();
        }

        public Room Navigate(string choice, DungeonIo dungeonIo)
        {
            choice = choice.ToLower();

            foreach (string keyword in exits.Keys)
            {
                if (choice.Contains(keyword))
                {
                    return exits[keyword];
                }
            }

            dungeonIo.WriteLine($"I do not understand what you mean by {choice}, please read the story more carefully!");
            return this;
        }


    }
}