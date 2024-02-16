using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dungeon.Logic.Data;

namespace Dungeon.Logic.Model
{
    public class Room
    {
        private Room()
        {

        }
        public Room(string name, string narrative, int roomId = 0)
        {
            Name = name;
            Narrative = narrative;
            RoomId = roomId;
        }

        public int RoomId { get; private set; }
        [Required]
        public string Narrative { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
        public bool EndOfGame => _exits.Count.Equals(0);

        private List<RoomExit> _exits = new List<RoomExit>();
        public IEnumerable<RoomExit> Exits => _exits.ToList();

        public Room WithExit(string exitphrase, Room exitRoom) 
        {
            _exits.Add(new RoomExit(exitphrase, exitRoom.RoomId));
            return this;
        }

        public Room Navigate(IStoryRepository repo, string phrase)
        {
            string[] words = phrase.Split(' ');

            foreach (RoomExit roomExit in Exits)
            {
                foreach (string word in words)
                {
                    if (word.Contains(roomExit.Keyword, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return repo.Navigate(roomExit.NavigateRoomId);
                    }
                }
            }

            return null;
        }

    }
}