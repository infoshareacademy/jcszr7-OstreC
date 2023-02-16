using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;

namespace OstreCWEB.Repository.DataBase
{
    public class OstreCWebContext : IdentityDbContext<User>
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
           
            base.OnModelCreating(builder);
            UserConfiguration(builder);
            ConfigureCharacters(builder);
            ConfigureActions(builder);
            ConfigureItems(builder);
            ConfigureStories(builder);
            ConfigureUsersParagraphs(builder);
            ConfigureManyToMany(builder);
            ConfigureUser(builder);
            ConfigureParagraphItems(builder);
            ConfigureClasses(builder);
        }
        private void ConfigureClasses(ModelBuilder builder)
        {
            builder.Entity<PlayableClass>().HasKey(x => x.PlayableClassId);

            builder.Entity<PlayableClass>()
                .HasMany(x => x.ActionsGrantedByClass)
                .WithOne(x => x.PlayableClass)
                .HasForeignKey(x => x.PlayableClassId);
           
            builder.Entity<PlayableClass>()
                .HasMany(x => x.ItemsGrantedByClass)
                .WithOne(x => x.PlayableClass)
                .HasForeignKey(x => x.PlayableClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
        private void ConfigureUser(ModelBuilder builder)
        {
            builder.Entity<User>().Navigation(e => e.UserParagraphs).AutoInclude(); 
        }
        private void ConfigureManyToMany(ModelBuilder builder)
        {
            builder.Entity<ItemCharacter>().Navigation(e => e.Item).AutoInclude();
            builder.Entity<ItemCharacter>().Navigation(e => e.Character).AutoInclude();

            builder.Entity<AbilitiesCharacter>().Navigation(e => e.CharacterAction).AutoInclude();
            builder.Entity<AbilitiesCharacter>().Navigation(e => e.Character).AutoInclude();

            builder.Entity<UserParagraph>().Navigation(e => e.Paragraph).AutoInclude();
            builder.Entity<UserParagraph>().Navigation(e => e.ActiveCharacter).AutoInclude();
            builder.Entity<UserParagraph>().Navigation(e => e.User).AutoInclude();
        }
        private void ConfigureItems(ModelBuilder builder)
        {
            builder.Entity<Item>().Navigation(e => e.ActionToTrigger).AutoInclude();
            builder.Entity<Item>()
                .HasOne(x => x.ActionToTrigger)
                .WithMany(x => x.LinkedItems)
                .HasForeignKey(x => x.ActionToTriggerId);
            //builder.Entity<ItemCharacter>()
            // .HasKey(x => new { x.ItemId, x.CharacterId }); 
            builder.Entity<ItemCharacter>()
                .HasOne(x => x.Item)
                .WithMany(x => x.LinkedCharacters)
                .HasForeignKey(x => x.ItemId);

            builder.Entity<ItemCharacter>()
                .HasOne(x => x.Character)
                .WithMany(x => x.LinkedItems)
                .HasForeignKey(x => x.CharacterId);
        }

        private void ConfigureActions(ModelBuilder builder)
        {
            builder.Entity<Ability>().Navigation(e => e.Status).AutoInclude();

            builder.Entity<Ability>()
                .HasMany(x => x.LinkedItems)
                .WithOne(x => x.ActionToTrigger)
                .HasForeignKey(x => x.ActionToTriggerId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Entity<AbilitiesCharacter>()
                .HasKey(x => new { x.CharacterId, x.CharacterActionId });

            builder.Entity<AbilitiesCharacter>()
                .HasOne(pt => pt.Character)
                .WithMany(p => p.LinkedAbilities)
                .HasForeignKey(pt => pt.CharacterId);

            builder.Entity<AbilitiesCharacter>()
                .HasOne(pt => pt.CharacterAction)
                .WithMany(t => t.LinkedCharacter)
                .HasForeignKey(pt => pt.CharacterActionId);
             
        }

        private void UserConfiguration(ModelBuilder builder)
        {
            builder.Entity<User>().Navigation(e => e.StoriesCreated).AutoInclude();
            builder.Entity<User>().Navigation(e => e.CharactersCreated).AutoInclude();
            builder.Entity<User>().Navigation(e => e.UserParagraphs).AutoInclude();
        }

        private void ConfigureCharacters(ModelBuilder builder)
        {
            builder.Entity<PlayableCharacter>().Navigation(e => e.CharacterClass).AutoInclude();
            builder.Entity<PlayableCharacter>().Navigation(e => e.Race).AutoInclude();
            builder.Entity<PlayableCharacter>().Navigation(e => e.LinkedAbilities).AutoInclude();
            builder.Entity<PlayableCharacter>().Navigation(e => e.LinkedItems).AutoInclude();

            builder.Entity<Character>().HasKey(entity => entity.CharacterId);
            builder.Entity<PlayableCharacter>()
                .HasOne(r => r.Race)
                .WithMany(p => p.PlayableCharacter)
                .HasForeignKey(x => x.RaceId);

            builder.Entity<PlayableCharacter>()
                .HasOne(c => c.CharacterClass)
                .WithMany(p => p.PlayableCharacter);
            builder.Entity<UserParagraph>()
                 .HasOne(x => x.ActiveCharacter)
                 .WithOne(x => x.UserParagraph)
                 .HasForeignKey<PlayableCharacter>(x => x.UserParagraphId)
                 .OnDelete(DeleteBehavior.ClientCascade);
        }

        private void ConfigureStories(ModelBuilder builder)
        {
            { //Story
                builder.Entity<Story>()
                    .HasMany(x => x.Paragraphs)
                    .WithOne(x => x.Story)
                    .HasForeignKey(x => x.StoryId);

                builder.Entity<Story>()
                    .HasOne(x => x.User)
                    .WithMany(x => x.StoriesCreated)
                    .HasForeignKey(x => x.UserId);
            } // Story

            { // Paragraph
                //Fight
                builder.Entity<Paragraph>()
                    .HasOne(x => x.FightProp)
                    .WithOne(x => x.Paragraph)
                    .HasForeignKey<FightProp>(x => x.ParagraphId);

                //Dialog
                builder.Entity<Paragraph>()
                    .HasOne(x => x.DialogProp)
                    .WithOne(x => x.Paragraph)
                    .HasForeignKey<DialogProp>(x => x.ParagraphId);

                //Shopkeeper
                builder.Entity<Paragraph>()
                    .HasOne(x => x.ShopkeeperProp)
                    .WithOne(x => x.Paragraph)
                    .HasForeignKey<ShopkeeperProp>(x => x.ParagraphId);

                //Test
                builder.Entity<Paragraph>()
                    .HasOne(x => x.TestProp)
                    .WithOne(x => x.Paragraph)
                    .HasForeignKey<TestProp>(x => x.ParagraphId);

                builder.Entity<Paragraph>()
                    .HasMany(x => x.Choices)
                    .WithOne(x => x.Paragraph)
                    .HasForeignKey(x => x.ParagraphId);
            } // Paragraph

            { // ParagraphFight
                builder.Entity<FightProp>()
                    .HasMany(x => x.ParagraphEnemies)
                    .WithOne(x => x.FightProp)
                    .HasForeignKey(x => x.FightPropId);

                builder.Entity<EnemyInParagraph>()
                    .HasOne(x => x.Enemy)
                    .WithMany(x => x.EnemyInParagraphs)
                    .HasForeignKey(x => x.EnemyId);
            } // ParagraphFight
        }

        private void ConfigureUsersParagraphs(ModelBuilder builder)
        { 
            builder.Entity<UserParagraph>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserParagraphs); 

            builder.Entity<UserParagraph>()
                .HasOne(x => x.Paragraph)
                .WithMany(x => x.UserParagraphs);  
            
        }

        private void ConfigureParagraphItems(ModelBuilder builder)
        {
            builder.Entity<ParagraphItem>()
                .HasKey(x => new { x.ItemId, x.ParagraphId });

            builder.Entity<ParagraphItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ParagraphItems);

            builder.Entity<ParagraphItem>()
                .HasOne(x => x.Paragraph)
                .WithMany(x => x.ParagraphItems);
        }
    } 
}     
