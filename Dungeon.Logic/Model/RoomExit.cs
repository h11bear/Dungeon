namespace Dungeon.Logic.Model
{
    public class RoomExit
    {

        public RoomExit(string keyword, string roomName)
        {
            Keyword = keyword;
            RoomName = roomName;
        }
        public string Keyword { get; private set; }
        public string RoomName { get; private set; }

    }
}