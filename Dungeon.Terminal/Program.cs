using System;
using System.Linq;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;

namespace Dungeon.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the Dungeon");
            Console.WriteLine();
            string characterName = null;

            while (string.IsNullOrEmpty(characterName))
            {
                Console.WriteLine("Please enter your name traveler");
                Console.WriteLine();

                characterName = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(characterName))
                {
                    WriteCriticalError("The nameless are not allowed passage to the dungeon!");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Welcome " + characterName);

            StoryXmlRepository storyXmlRepository = new StoryXmlRepository(@"..\Dungeon.Logic\Story\MainDungeon.xml");

            var dungeonStory = storyXmlRepository.GetStory("main");
            dungeonStory.Begin();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(dungeonStory.Narrative);
                Console.WriteLine();

                if (dungeonStory.EndOfGame)
                {
                    Console.WriteLine($"Thanks for playing {characterName}!");
                    Environment.Exit(0);
                }

                string choice = Console.ReadLine();

                try
                {
                    dungeonStory.Navigate(choice);
                }
                catch (NavigationException navEx)
                {
                    WriteCriticalError(navEx.Message);
                }
                catch (RoomNotFoundException roomEx)
                {
                    WriteCriticalError(roomEx.Message);
                    Environment.Exit(1);
                }

            }
        }

        private static void WriteCriticalError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
