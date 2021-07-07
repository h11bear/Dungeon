using Dungeon.Logic.Input;
using System.Collections.Generic;

namespace Dungeon.Logic.Model
{
    public class RoomExit
    {

        public RoomExit(string keyword, string roomName)
        {
            Keyword = keyword;
            RoomName = roomName;
        }
        public string Keyword { get; }
        public string RoomName { get; }

    }
}