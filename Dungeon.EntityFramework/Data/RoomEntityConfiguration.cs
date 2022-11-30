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
            // exits are owned by the room, can use [Owned] attribute on entity
            // but would require direct reference to SQL Server entity framework in logic project that I am trying to avoid
            builder.OwnsMany(room => room.Exits, re => {
                re.WithOwner().HasForeignKey("RoomId");
                re.Property<int>("Id");
                re.HasKey("Id");
            });
        }
    }
}