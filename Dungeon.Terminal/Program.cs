using System;
using Dungeon.Logic.Model;

namespace Dungeon.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dungeon");
            Console.WriteLine("Please Enter Your Name");
            var characterName = Console.ReadLine();
            Console.WriteLine("Welcome " + characterName);
            var terminalIo = new TerminalIo();
            var dungeonStory = new DungeonStory(characterName, terminalIo);
            dungeonStory.Start();
        }

    }
}
