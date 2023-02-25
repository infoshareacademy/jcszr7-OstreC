using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.StoryModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class EnemyInParagraphConfiguration : IEntityTypeConfiguration<EnemyInParagraph>
    {
        public void Configure(EntityTypeBuilder<EnemyInParagraph> builder)
        {
            builder
                    .HasOne(x => x.Enemy)
                    .WithMany(x => x.EnemyInParagraphs)
                    .HasForeignKey(x => x.EnemyId);
        }
    }
}
