
using Dungeon.Logic.Input;

namespace Dungeon.Logic.Model
{
    public class DungeonStory
    {
        private string _characterName;
        private RoomCatalog _roomCatalog;
        public DungeonStory(string characterName, RoomCatalog roomCatalog)
        {
            _characterName = characterName;
            _roomCatalog = roomCatalog;
        }


        public void Start(DungeonIo dungeonIo)
        {
            Room currentRoom = _roomCatalog.GetEntrance();
            currentRoom.Enter(dungeonIo);

            while (true)
            {
                var choice = dungeonIo.ReadLine();

                RoomExit roomExit = currentRoom.Navigate(choice);
                if (roomExit == null)
                {
                    dungeonIo.WriteLine($"I do not understand what you mean by {choice}, please read the story more carefully!");
                }
                else
                {
                    currentRoom = _roomCatalog.Find(roomExit.RoomName);

                    if (currentRoom == null)
                    {
                            dungeonIo.WriteLine($"Sorry, the programmer made a mistake!  Could not find room {roomExit.RoomName}!");
                            dungeonIo.Exit(1);
                    }
                    else
                    {
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
    }
}