using System;

namespace Dungeon.Logic.Model
{
    public class NavigationException: Exception
    {

        public NavigationException(string message): base(message)
        {

        }

    }
}