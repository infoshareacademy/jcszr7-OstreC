using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWeb.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.Configurations
{
    public class ParagraphItemConfiguration : IEntityTypeConfiguration<ParagraphItem>
    {
        public void Configure(EntityTypeBuilder<ParagraphItem> builder)
        {
            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.ParagraphItems)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasOne(x => x.Paragraph)
                .WithMany(x => x.ParagraphItems)
                .HasForeignKey(x => x.ParagraphId);
        }
    }
}