using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class ParagraphConfiguration : IEntityTypeConfiguration<Paragraph>
    {
        public void Configure(EntityTypeBuilder<Paragraph> builder)
        {
            builder
                   .HasOne(x => x.FightProp)
                   .WithOne(x => x.Paragraph)
                   .HasForeignKey<FightProp>(x => x.ParagraphId);

            //Dialog
            builder
                .HasOne(x => x.DialogProp)
                .WithOne(x => x.Paragraph)
                .HasForeignKey<DialogProp>(x => x.ParagraphId);

            //Shopkeeper
            builder
                .HasOne(x => x.ShopkeeperProp)
                .WithOne(x => x.Paragraph)
                .HasForeignKey<ShopkeeperProp>(x => x.ParagraphId);

            //Test
            builder
                .HasOne(x => x.TestProp)
                .WithOne(x => x.Paragraph)
                .HasForeignKey<TestProp>(x => x.ParagraphId);

            builder
                .HasMany(x => x.Choices)
                .WithOne(x => x.Paragraph)
                .HasForeignKey(x => x.ParagraphId);
        }
    }
}
