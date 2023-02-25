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
    public class ItemCharacterConfiguration : IEntityTypeConfiguration<ItemCharacter>
    {
        public void Configure(EntityTypeBuilder<ItemCharacter> builder)
        {
            builder.Navigation(e => e.Item).AutoInclude();
            builder.Navigation(e => e.Character).AutoInclude();

            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.LinkedCharacters)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasOne(x => x.Character)
                .WithMany(x => x.LinkedItems)
                .HasForeignKey(x => x.CharacterId);
        }
    }
}
