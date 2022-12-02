using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model
{
    public class RoomExit
    {

        public RoomExit(string keyword, string roomName)
        {
            Keyword = keyword;
            RoomName = roomName;
        }
        
        [Required]
        [MaxLength(50)]
        public string Keyword { get; private set; }
        [Required]
        [MaxLength(50)]
        public string RoomName { get; private set; }

    }
}