using Microsoft.EntityFrameworkCore;
using Dungeon.Logic.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dungeon.EntityFramwork.Data 
{
    public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
    {
        // https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.OwnsMany(room => room.Exits);
        }
    }
}