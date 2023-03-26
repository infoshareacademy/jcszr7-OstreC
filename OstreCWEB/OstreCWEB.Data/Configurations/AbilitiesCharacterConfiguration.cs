using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class AbilitiesCharacterConfiguration : IEntityTypeConfiguration<AbilitiesCharacter>
    {
        public void Configure(EntityTypeBuilder<AbilitiesCharacter> builder)
        {
            //builder.Navigation(e => e.CharacterAction).AutoInclude();
            //builder.Navigation(e => e.Character).AutoInclude();

            builder
               .HasKey(x => new { x.CharacterId, x.CharacterActionId });

            builder
                .HasOne(pt => pt.Character)
                .WithMany(p => p.LinkedAbilities)
                .HasForeignKey(pt => pt.CharacterId);

            builder
                .HasOne(pt => pt.CharacterAction)
                .WithMany(t => t.LinkedCharacter)
                .HasForeignKey(pt => pt.CharacterActionId);
        }
    }
}
