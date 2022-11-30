using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model
{
    public class RoomCatalog
    {
        private List<Room> rooms = new List<Room>();

        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
        public int RoomCatalogId {get; private set;}
        public RoomCatalog(int roomCatalogId, string name)
        {
            RoomCatalogId = roomCatalogId;
            Name = name;
        }

        public RoomCatalog(string name)
        {
            Name = name;
        }

        public void AddRoom(Room room) 
        {
            rooms.Add(room);
        }

        public Room GetEntrance()
        {
            return rooms[0];
        }

        public Room Find(string name) 
        {
            return rooms.Find(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}