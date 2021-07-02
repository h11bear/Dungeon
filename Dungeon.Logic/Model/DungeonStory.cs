
using Dungeon.Logic.Input;

namespace Dungeon.Logic.Model
{
    public class DungeonStory
    {
        private Room _dungeonEntrance;
        private string _characterName;
        public DungeonStory(string characterName)
        {
            _characterName = characterName;
            _dungeonEntrance = new Room("You find youself in a dark room. You can open the door, explore, or follow the line of torches.");

            Room rockRoom = new Room("You open the door and find a rock blocking your path. Do you move the rock? Or squeeze by it and continue on the path?");
            Room pitRoom = new Room("You look around the room and see a faint light glowing around the corner. There is also what looks like a bottomless pit across the room. What do you inspect first?");
            Room torchRoom = new Room("You follow the line of torches and are greeted with a huge brown door.\n\nYou carefully open it and see a mossy green monster staring at you hungrily!\n\nDo you fight it or run away?");
            Room trollRoom = new Room("You run up to the troll and punch it in the nose before it has time to react. The troll falls back, temporarily stunned. You use its club that it was holding to smash its skull in.");
            Room scaredyCatRoom = new Room("What a chicken!  The troll grabs you and locks you in a prison cell, rot in hell.");
            Room moveRockroom = new Room("You move the rock and see a bright blue sky with billowing white clouds. You've escaped!");
            Room monsterDoorRoom = new Room(@"You squeeze past the rock and continue on your journey to try and escape this dungeon. You notice that there are some torches
lining the walls. You look up and see a giant snake with two heads staring at you! Do you fight or try to turn around?");
           Room choiceRoom = new Room("Next to you is a sword and a magic wand. Which do you choose to use agaist the beastly snake?");
           Room eatToDeathRoom = new Room(@"You turn around and begin running back the way you came. The snake grabs your shirt with it's mouth from behind and swallows you whole.You coward.");
           Room magicWandRoom = new Room("You pick up the magic wand and try to zap a lightning bolt at the snake. The wand falters and instead electricutes you.You are fried.");
           Room swordRoom = new Room(@"You grab the sword, run up to the snake, jump, and slice off its head before it has time to react.
You successfully slay the beast. Do you climb up the ladder at the back of the room or do you explore?");
           Room elephantRoom = new Room(@"You climb up the tall ladder and step onto the creaky wooden floor. You gasp. A gigantic elephant with mean red eyes stares at you. 
Do you fight it? Or try to get back down the ladder and look for another exit.");
           Room babySnakeRoom = new Room(@"You look around the room and see a giant web-like structure in the far left corner. You walk up to it and poke it with your sword.
Thousands of tiny baby snakes come pouring out of the structure. You try to fight them off but there are too may. They eat you alive.");
            Room fightRoom = new Room ("You charge torwards the elephant with your sword still in hand. The elephant sucks you up with its trunk before you are able to stab it. You die.");
           Room runAwayRoom = new Room ("You turn around and try to climb down the ladder as fast as you can. It begins to rock and you fall off. The ladder goes down with you and squishes you. You die");
            _dungeonEntrance.AddExit("door", rockRoom);
            _dungeonEntrance.AddExit("explore", pitRoom);
            _dungeonEntrance.AddExit("torch", torchRoom);

            torchRoom.AddExit("fight", trollRoom);
            torchRoom.AddExit("run", scaredyCatRoom);
            rockRoom.AddExit("move", moveRockroom);
            rockRoom.AddExit("squeeze" ,monsterDoorRoom);
            monsterDoorRoom.AddExit("fight",choiceRoom);
            monsterDoorRoom.AddExit("turn",eatToDeathRoom);
            choiceRoom.AddExit("wand",magicWandRoom);
            choiceRoom.AddExit("sword",swordRoom);
            swordRoom.AddExit("ladder",elephantRoom);
            swordRoom.AddExit("explore",babySnakeRoom);
            elephantRoom.AddExit("ladder",runAwayRoom);
            elephantRoom.AddExit("fight",fightRoom);
        }
        

        public void Start(DungeonIo dungeonIo)
        {
            Room currentRoom = _dungeonEntrance;
            currentRoom.Enter(dungeonIo);

            while (true)
            {
                var choice = dungeonIo.ReadLine();

                currentRoom = currentRoom.Navigate(choice, dungeonIo);
                currentRoom.Enter(dungeonIo);

                if (currentRoom.EndOfGame)
                {
                    dungeonIo.WriteLine($"Thanks for playing {_characterName}!");
                    dungeonIo.Exit(0);
                }
            }
        }
    }
}