using System;

namespace Dungeon
{
    public class DungeonStory
    {
        public void Start()
        {
            Console.WriteLine("You find youself in a dark room. You can open the door, explore, or follow the line of torches.");
            var choice = Console.ReadLine();
            if (choice.ToLower().Contains("door"))
            {
                OpenDoor();

            }
            else if (choice.ToLower().Contains("explore"))
            {

                Explore();
            }
        }
        public void OpenDoor()
        {
            Console.WriteLine("You open the door and find a rock blocking your path. Do you move the rock? Or squeeze by it and continue on the path?");
            var choice = Console.ReadLine();

        }

        public void Explore()
        {
            Console.WriteLine(@"You look around the room and see a faint light glowing around the corner.
                 There is also what looks like a bottomless pit across the room. What do you inspect first?");
            var choice = Console.ReadLine();
            if (choice.ToLower().Contains("light"))
            {
                FollowLight();

            }
            else if (choice.ToLower().Contains("pit"))
            {
                LookAtPit();

            }

        }
        public void FollowLight()
        {

        }
        public void LookAtPit()
        {

        }
    }
}