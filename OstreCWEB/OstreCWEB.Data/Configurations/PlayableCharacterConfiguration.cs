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
    public class PlayableCharacterConfiguration : IEntityTypeConfiguration<PlayableCharacter>
    {
        public void Configure(EntityTypeBuilder<PlayableCharacter> builder)
        {
            //    builder.Navigation(e => e.CharacterClass).AutoInclude();
            //    builder.Navigation(e => e.Race).AutoInclude();
            //    builder.Navigation(e => e.LinkedAbilities).AutoInclude();
            //    builder.Navigation(e => e.LinkedItems).AutoInclude();

            builder
               .HasOne(r => r.Race)
               .WithMany(p => p.PlayableCharacter)
               .HasForeignKey(x => x.RaceId);

            builder
                .HasOne(c => c.CharacterClass)
                .WithMany(p => p.PlayableCharacter);
        }
    }
}
