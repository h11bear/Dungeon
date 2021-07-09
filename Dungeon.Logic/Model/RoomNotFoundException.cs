using System;

namespace Dungeon.Logic.Model
{
    public class RoomNotFoundException: Exception
    {

        public RoomNotFoundException(string message): base(message)
        {

        }

    }
}