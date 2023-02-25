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
    public class PlayableClassConfiguration : IEntityTypeConfiguration<PlayableClass>
    {
        public void Configure(EntityTypeBuilder<PlayableClass> builder)
        {
            builder.HasKey(x => x.PlayableClassId);

            builder
                .HasMany(x => x.ActionsGrantedByClass)
                .WithOne(x => x.PlayableClass)
                .HasForeignKey(x => x.PlayableClassId);

            builder
                .HasMany(x => x.ItemsGrantedByClass)
                .WithOne(x => x.PlayableClass)
                .HasForeignKey(x => x.PlayableClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
