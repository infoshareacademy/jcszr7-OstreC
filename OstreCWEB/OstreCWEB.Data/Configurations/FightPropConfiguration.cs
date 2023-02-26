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
    public class FightPropConfiguration : IEntityTypeConfiguration<FightProp>
    {
        public void Configure(EntityTypeBuilder<FightProp> builder)
        {
            builder
                    .HasMany(x => x.ParagraphEnemies)
                    .WithOne(x => x.FightProp)
                    .HasForeignKey(x => x.FightPropId);
        }
    }
}
