using System;

namespace Dungeon.Logic
{
    public class DungeonStory
    {
        private Room _dungeonEntrance = new Room("You find youself in a dark room. You can open the door, explore, or follow the line of torches.");
        private string _characterName;
        public DungeonStory(string characterName) {
            _characterName = characterName;
            Room rockRoom = new Room("You open the door and find a rock blocking your path. Do you move the rock? Or squeeze by it and continue on the path?");
            Room pitRoom = new Room("You look around the room and see a faint light glowing around the corner. There is also what looks like a bottomless pit across the room. What do you inspect first?");
            Room torchRoom = new Room("You follow the line of torches and are greeted with a huge brown door. You carefully open it and see a mossy green moster staring at you hungrily!  Do you fight it or run away?");
            Room gorgonRoom = new Room("You see a scary gorgon!  It stares at you and you turn to stone!");
            Room scaredyCatRoom = new Room("What a chicken!  You are stuck in a prison room, rot in hell.");

            _dungeonEntrance.AddExit("door", rockRoom);
            _dungeonEntrance.AddExit("explore", pitRoom);
            _dungeonEntrance.AddExit("torch", torchRoom);

            torchRoom.AddExit("fight", gorgonRoom);
            torchRoom.AddExit("run", scaredyCatRoom);
        }

        public void Start()
        {
            Room currentRoom = _dungeonEntrance;
            currentRoom.Enter();
            
            while(true)
            {
                var choice = Console.ReadLine();
                
                currentRoom = currentRoom.Navigate(choice);
                currentRoom.Enter();

                if (currentRoom.EndOfGame) {
                    Console.WriteLine($"Thanks for playing {_characterName}!");
                    Environment.Exit(0);
                }
            }
        }
    }
}