using System.Collections.Generic;

namespace Dungeon.Logic
{
    public class Room
    {

        private DungeonIo _dungeonIo;
        public Room(string narrative, DungeonIo dungeonIo)
        {
            Narrative = narrative;
            _dungeonIo = dungeonIo;
        }
        public string Narrative { get; }
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

        private Dictionary<string, Room> exits = new Dictionary<string, Room>();

        public void Enter()
        {
            _dungeonIo.WriteLine();
            _dungeonIo.WriteLine(Narrative);
            _dungeonIo.WriteLine();
        }

        public Room Navigate(string choice)
        {
            choice = choice.ToLower();

            foreach(string keyword in exits.Keys) {
                if (choice.Contains(keyword)) 
                {
                    return exits[keyword];
                }
            }

            _dungeonIo.WriteLine($"I do not understand what you mean by {choice}, please read the story more carefully!");
            return this;
        }


    }
}