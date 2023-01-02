﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.Repository.Characters.CoreClasses;
using OstreCWEB.Data.Repository.Characters.MetaTags;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.StoryModels;
using OstreCWEB.Data.Repository.StoryModels.Properties;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace OstreCWEB.Data.DataBase
{
    public class OstreCWebContext : IdentityDbContext<User>
    {
        //User
        public DbSet<User> Users { get; set; }
        //public DbSet<PlayableCharacter> PlayableCharacters { get; set; }
        //public DbSet<Enemy> Enemies { get; set; }

        //Story

        //public DbSet<Story> Stories { get; set; }
        //public DbSet<Paragraph> Paragraphs { get; set; }
        //public DbSet<NextParagraph> NextParagraphs { get; set; }
        //public DbSet<FightProp> FightProps { get; set; }
        //public DbSet<TestProp> TestProps { get; set; }
        //public DbSet<DialogProp> DialogProps { get; set; }
        //public DbSet<ShopkeeperProp> ShopkeeperProps { get; set; }
        //public DbSet<EnemyInParagraph> EnemyInParagraphs { get; set; }

        //Character
        //public DbSet<PlayableCharacter> PlayableCharacters { get; set; }
        //public DbSet<Enemy> Enemies { get; set; }
        //Combat


        public OstreCWebContext() { 
        
        }
        public OstreCWebContext(DbContextOptions<OstreCWebContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    UserConfiguration(builder);
        //    ConfigureCharacters(builder);  
        //}

        //private void UserConfiguration(ModelBuilder builder)
        //{

        //    builder.Entity<User>()
        //            .HasMany(x => x.CharactersCreated)
        //            .WithOne(x => x.User)
        //            .HasForeignKey(x => x.UserId);
        //}
        //private void ConfigureCharacters(ModelBuilder builder)
        //{
            

        //    builder.Entity<PlayableCharacter>()
        //        .HasOne(r => r.Race)
        //        .WithMany(p => p.PlayableCharacter)
        //        .HasForeignKey(x => x.RaceId); 
        //    builder.Entity<PlayableCharacter>()
        //        .HasOne(c => c.CharacterClass)
        //        .WithMany(p => p.PlayableCharacter);

        //    //builder.Entity<ItemCharacter>()
        //    //    .HasKey(x => new { x.ItemId, x.PlayableCharacterId });

        //    builder.Entity<ItemCharacter>()
        //        .HasOne(x => x.Item)
        //        .WithMany(x => x.ItemCharacter)
        //        .HasForeignKey(x => x.ItemId);

        //    builder.Entity<ItemCharacter>()
        //      .HasOne(x => x.PlayableCharacter)
        //      .WithMany(x => x.ItemCharacter)
        //      .HasForeignKey(x => x.PlayableCharacterId);

        //}
        //private void ConfigureStories( ModelBuilder builder)
        //{
        //    { //Story
        //        builder.Entity<Story>()
        //            .HasMany(x => x.Paragraphs)
        //            .WithOne(x => x.Story)
        //            .HasForeignKey(x => x.StoryId);
        //    } // Story

        //    { // Paragraph
        //        builder.Entity<Paragraph>()
        //            .HasOne(x => x.FightProp)
        //            .WithOne(x => x.Paragraph)
        //            .HasForeignKey<Paragraph>(x => x.FightPropId);

        //        builder.Entity<Paragraph>()
        //            .HasOne(x => x.TestProp)
        //            .WithOne(x => x.Paragraph)
        //            .HasForeignKey<Paragraph>(x => x.TestPropId);

        //        builder.Entity<Paragraph>()
        //            .HasOne(x => x.DialogProp)
        //            .WithOne(x => x.Paragraph)
        //            .HasForeignKey<Paragraph>(x => x.DialogPropId);

        //        builder.Entity<Paragraph>()
        //            .HasOne(x => x.ShopkeeperProp)
        //            .WithOne(x => x.Paragraph)
        //            .HasForeignKey<Paragraph>(x => x.ShopkeeperPropId);
        //    } // Paragraph

        //    { // ParagraphFight
        //        builder.Entity<FightProp>()
        //            .HasMany(x => x.ParagraphEnemies)
        //            .WithOne(x => x.FightProp)
        //            .HasForeignKey(x => x.FightPropId);
        //    } // ParagraphFight
        //}
       

    } 
}
