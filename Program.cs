using System;

namespace Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dungeon");
            Console.WriteLine("Please Enter Your Name");
            var characterName = Console.ReadLine();
            Console.WriteLine("Welcome " + characterName);
    
            var dungeonStory = new DungeonStory(characterName);
            dungeonStory.Start();
        }

    }
}
