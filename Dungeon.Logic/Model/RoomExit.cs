using Dungeon.Logic.Input;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class RoomExit
    {

        public RoomExit(string keyword, string room)
        {
            Keyword = keyword;
            Room = room;
        }
        public string Keyword { get; }
        public string Room { get; }

    }
}