using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;
using System.Reflection.Emit;

namespace OstreCWEB.Repository.DataBase
{
    public class OstreCWebContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        //Relations many to many
        public DbSet<AbilitiesCharacter> ActionCharactersRelation { get; set; }
        public DbSet<ItemCharacter> ItemsCharactersRelation { get; set; }
        public DbSet<UserParagraph> UserParagraphs { get; set; }
        public DbSet<ParagraphItem> ParagraphItems { get; set; }

        //User
        public DbSet<User> Users { get; set; }

        //Characters
        public DbSet<PlayableCharacter> PlayableCharacters { get; set; }

        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<PlayableClass> PlayableCharacterClasses { get; set; }
        public DbSet<PlayableRace> PlayableCharacterRaces { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Ability> CharacterActions { get; set; }

        //Story
        public DbSet<Story> Stories { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<FightProp> FightProps { get; set; }
        public DbSet<TestProp> TestProps { get; set; }
        public DbSet<DialogProp> DialogProps { get; set; }
        public DbSet<ShopkeeperProp> ShopkeeperProps { get; set; }
        public DbSet<EnemyInParagraph> EnemyInParagraphs { get; set; }

        //Combat

        public OstreCWebContext()
        {
        }

        public OstreCWebContext(DbContextOptions<OstreCWebContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(builder);
        }
    } 
}     
