using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            //builder.Navigation(e => e.Status).AutoInclude();

            builder
                .HasMany(x => x.LinkedItems)
                .WithOne(x => x.Ability)
                .HasForeignKey(x => x.AbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
