using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OstreCWEB.DomainModels.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class ParagraphItemConfiguration : IEntityTypeConfiguration<ParagraphItem>
    {
        public void Configure(EntityTypeBuilder<ParagraphItem> builder)
        {
            builder
                .HasKey(x => new { x.ItemId, x.ParagraphId });

            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.ParagraphItems);

            builder
                .HasOne(x => x.Paragraph)
                .WithMany(x => x.ParagraphItems);
        }
    }
}
