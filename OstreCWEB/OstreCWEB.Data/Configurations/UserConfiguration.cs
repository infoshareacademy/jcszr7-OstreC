using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OstreCWEB.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.Navigation(e => e.UserParagraphs).AutoInclude();
            //builder.Navigation(e => e.StoriesCreated).AutoInclude();
            //builder.Navigation(e => e.CharactersCreated).AutoInclude();
            //builder.Navigation(e => e.UserParagraphs).AutoInclude();
        }
    }
}
