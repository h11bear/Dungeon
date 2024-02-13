using System.ComponentModel.DataAnnotations;

namespace Dungeon.Logic.Model;
/// <summary>
/// 
/// </summary>
/// <remarks>
/// To use complex types as value objects in EF Core 8, you need to do the following steps:
/// Define your value object class as a readonly struct or a class with only readonly properties.
/// Use the OwnsOne method in the OnModelCreating method of your DbContext class to configure the owned entity type mapping for your value object.
/// Optionally, you can use the HasConversion method to specify how your value object should be stored in the database.
/// </remarks>
/// <see cref="https://medium.com/@nikhiltjha/how-ef-core-8-fixes-value-objects-d221c0791c44"/>
public class RoomExit
{

    private RoomExit()
    {

    }

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
