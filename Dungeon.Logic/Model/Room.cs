using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dungeon.Logic.Model
{
    public class Room
    {
        private Room()
        {

        }
        public Room(string name, string narrative, IEnumerable<RoomExit> exits)
        {
            Name = name;
            Narrative = narrative;
            if (exits != null)
            {
                _exits.AddRange(exits);
            }
        }

        public int RoomId { get; private set; }
        [Required]
        public string Narrative { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
        public bool EndOfGame
        {
            get
            {
                return _exits.Count.Equals(0);
            }
        }

        private List<RoomExit> _exits = new List<RoomExit>();
        public IEnumerable<RoomExit> Exits => _exits?.ToList();

        public RoomExit Navigate(string phrase)
        {
            string[] words = phrase.Split(' ');

            foreach (RoomExit roomExit in Exits)
            {
                foreach (string word in words)
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