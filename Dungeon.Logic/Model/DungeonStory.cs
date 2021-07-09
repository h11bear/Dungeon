
namespace Dungeon.Logic.Model
{
    public class DungeonStory
    {
        private RoomCatalog _roomCatalog;
        public DungeonStory(RoomCatalog roomCatalog)
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
        }

        public void Navigate(string phrase) 
        {
            RoomExit roomExit = CurrentRoom.Navigate(phrase);
            if (roomExit == null) 
            {
                throw new NavigationException($"I do not understand what you mean by {phrase}, please read the story more carefully!");
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