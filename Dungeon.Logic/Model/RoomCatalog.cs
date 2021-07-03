using System;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class RoomCatalog
    {
        private List<Room> rooms = new List<Room>();
        public string Name { get; }  
        public RoomCatalog(string name)
        {
            Name = name;
        }

        public void AddRoom(Room room) 
        {
            rooms.Add(room);
        }

        public Room Find(string name) 
        {
            return rooms.Find(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}