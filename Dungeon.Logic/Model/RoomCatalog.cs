using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon.Logic.Model
{
    public class RoomCatalog
    {
        private RoomCatalog()
        {

        }
        public RoomCatalog(IEnumerable<Room> rooms)
        {
            this.Rooms = rooms;
        }

        public IEnumerable<Room> Rooms {get; private set;}

        public Room GetEntrance()
        {
            return Rooms.First();
        }

        public Room Find(string name) 
        {
            return Rooms.FirstOrDefault(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}