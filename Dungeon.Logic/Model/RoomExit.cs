using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model
{
    public class RoomExit
    {

        public RoomExit(string keyword, Room exitRoom)
        {
            Keyword = keyword;
            ExitRoom = exitRoom;
        }
        
        [Required]
        [MaxLength(50)]
        public string Keyword { get; private set; }
        [Required]
        public Room ExitRoom { get; private set; }

    }
}