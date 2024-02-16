using Microsoft.EntityFrameworkCore;
using Dungeon.Logic.Model;
using Microsoft.Extensions.Configuration;

namespace Dungeon.EntityFramework.Data;

public interface IDungeonContext
{
    DbSet<Room> Rooms { get; set; }
    DbSet<Story> Stories { get; set; }
}

//learn entity framework core
//https://medium.com/@darshana-edirisinghe/entity-framework-performance-improvement-section-1-different-loading-mechanisms-in-entity-3e3ce2affee6#:~:text=To%20disable%20lazy%20loading%20in,Here's%20an%20example.&text=In%20this%20example%2C%20the%20Customer,will%20not%20be%20lazy%2Dloaded.

public class DungeonContext : DbContext, IDungeonContext
{
    private IConfiguration _configuration { get; }
    private string _connectionString;

    public DungeonContext(IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        string? dbConnectionString = configuration.GetConnectionString("DungeonDbConnectionString");
        if (dbConnectionString == null)
        {
            throw new InvalidOperationException("DungeonDbConnectionString is not configured");
        }
        _connectionString = dbConnectionString;

        _configuration = configuration;
    }


    public DbSet<Room> Rooms { get; set; } = null!;  // null forgiving operator

    public DbSet<Story> Stories { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //turn off lazy loading for all entities
        modelBuilder.ApplyConfiguration(new StoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());

        // https://stackoverflow.com/questions/46497733/using-singular-table-names-with-ef-core-2
        // Use the entity name instead of the Context.DbSet<T> name
        // refs https://learn.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=fluent-api#table-name

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // owned entity types like RoomExit throw an exception when attempting to configure table name
            if (!entityType.IsOwned())
            {
                //ToTable maps the plural collection name to the singular database table name
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }
        
        //TODO: investigate where lazy loading is applied and if this is necessary, cannot disable it here, throws exception
        //ChangeTracker.LazyLoadingEnabled = false;
    }
}

