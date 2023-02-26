using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.StoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder
                    .HasMany(x => x.Paragraphs)
                    .WithOne(x => x.Story)
                    .HasForeignKey(x => x.StoryId);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.StoriesCreated)
                .HasForeignKey(x => x.UserId);
        }
    }
}
