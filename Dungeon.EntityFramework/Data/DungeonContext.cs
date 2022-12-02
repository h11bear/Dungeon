using Microsoft.EntityFrameworkCore;
using Dungeon.Logic.Model;
using Microsoft.Extensions.Configuration;

namespace Dungeon.EntityFramwork.Data 
{
    public class DungeonContext: DbContext
    {
        protected IConfiguration Configuration { get; }
        public DungeonContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public DbSet<RoomCatalog>? RoomCatalogs { get; set; }
        public DbSet<Room>? Rooms { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DungeonDbConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());

            // https://stackoverflow.com/questions/46497733/using-singular-table-names-with-ef-core-2
            // Use the entity name instead of the Context.DbSet<T> name
            // refs https://learn.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=fluent-api#table-name

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // owned entity types like RoomExit throw an exception when attempting to configure table name
                if (!entityType.IsOwned())
                {
                    modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
                }
            }

            // modelBuilder.Entity<Room>()
            //     .HasData(new Room())
        }
    }

}