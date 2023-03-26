using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class UserParaghraphConfiguration : IEntityTypeConfiguration<UserParagraph>
    {
        public void Configure(EntityTypeBuilder<UserParagraph> builder)
        {
            //builder.Navigation(e => e.Paragraph).AutoInclude();
            //builder.Navigation(e => e.ActiveCharacter).AutoInclude();
            //builder.Navigation(e => e.User).AutoInclude();

            builder
                 .HasOne(x => x.ActiveCharacter)
                 .WithOne(x => x.UserParagraph)
                 .HasForeignKey<PlayableCharacter>(x => x.UserParagraphId)
                 .OnDelete(DeleteBehavior.ClientCascade);

            builder
               .HasOne(x => x.User)
               .WithMany(x => x.UserParagraphs);

            builder
                .HasOne(x => x.Paragraph)
                .WithMany(x => x.UserParagraphs);
        }
    }
}
