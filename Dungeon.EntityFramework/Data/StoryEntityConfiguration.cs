using Microsoft.EntityFrameworkCore;
using Dungeon.Logic.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dungeon.EntityFramework.Data;
public class StoryEntityConfiguration : IEntityTypeConfiguration<Story>
{
    // https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        //eager loads the entrance property on the story object
        builder.Navigation(story => story.Entrance).AutoInclude();
    }
}
