
namespace Dungeon.Logic.Model
{
    public class DungeonStoryLegacy
    {
        
        private RoomCatalog _roomCatalog;
        public DungeonStoryLegacy(RoomCatalog roomCatalog)
        {
            _roomCatalog = roomCatalog;
        }

        public string Narrative
        {
            get
            {
                return CurrentRoom.Narrative;
            }
        }

        public void Begin()
        {
            CurrentRoom = _roomCatalog.GetEntrance();
            IsEntrance = true;
        }

        public bool IsEntrance { get; private set; }

        public void Navigate(string keyword)
        {
            RoomExit roomExit = CurrentRoom.Navigate(keyword);
            if (roomExit == null)
            {
                throw new NavigationException($"I do not understand what you mean by {keyword}, please read the story more carefully!");
            }
            else
            {
                EnterRoom(roomExit.RoomName);
            }
        }

        private void EnterRoom(string roomName)
        {
            CurrentRoom = _roomCatalog.Find(roomName);

            if (CurrentRoom == null)
            {
                throw new RoomNotFoundException($"Sorry, the programmer made a mistake!  Could not find room {roomName}!");
            }
        }
        public Room CurrentRoom { get; private set; }

        public void Resume(string roomName)
        {
            EnterRoom(roomName);
        }

        public bool EndOfGame
        {
            get
            {
                return CurrentRoom.EndOfGame;
            }
        }
    }
}