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
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Navigation(e => e.ActionToTrigger).AutoInclude();
            builder
                .HasOne(x => x.ActionToTrigger)
                .WithMany(x => x.LinkedItems)
                .HasForeignKey(x => x.ActionToTriggerId);

            //builder.Entity<ItemCharacter>()
            // .HasKey(x => new { x.Id, x.Id }); 
        }
    }
}
