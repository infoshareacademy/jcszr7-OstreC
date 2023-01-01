﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OstreCWEB.Data.DataBase;

#nullable disable

namespace OstreCWEB.Data.Migrations
{
    [DbContext(typeof(OstreCWebContext))]
    [Migration("20230101092256_fixedIdentityLoggin")]
    partial class fixedIdentityLoggin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacterId"), 1L, 1);

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Charisma")
                        .HasColumnType("int");

                    b.Property<int>("Constitution")
                        .HasColumnType("int");

                    b.Property<int>("CurrentHealthPoints")
                        .HasColumnType("int");

                    b.Property<int>("Dexterity")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxHealthPoints")
                        .HasColumnType("int");

                    b.Property<int>("Strenght")
                        .HasColumnType("int");

                    b.Property<int>("Wisdom")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.ToTable("Character");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Character");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.CharacterAction", b =>
                {
                    b.Property<int>("CharacterActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacterActionId"), 1L, 1);

                    b.Property<string>("ActionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

                    b.Property<bool>("AggressiveAction")
                        .HasColumnType("bit");

                    b.Property<int>("Flat_Dmg")
                        .HasColumnType("int");

                    b.Property<int>("Hit_Dice_Nr")
                        .HasColumnType("int");

                    b.Property<bool>("InflictsStatus")
                        .HasColumnType("bit");

                    b.Property<int>("Max_Dmg")
                        .HasColumnType("int");

                    b.Property<string>("PossibleTargets")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SavingThrowPossible")
                        .HasColumnType("bit");

                    b.Property<int>("StatForTest")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UsesLeftBeforeRest")
                        .HasColumnType("int");

                    b.Property<int>("UsesMax")
                        .HasColumnType("int");

                    b.Property<int>("UsesMaxBeforeRest")
                        .HasColumnType("int");

                    b.HasKey("CharacterActionId");

                    b.HasIndex("StatusId");

                    b.ToTable("CharacterActions");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<int?>("ActionToTriggerCharacterActionId")
                        .HasColumnType("int");

                    b.Property<int?>("ArmorClass")
                        .HasColumnType("int");

                    b.Property<int?>("ArmorType")
                        .HasColumnType("int");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("ActionToTriggerCharacterActionId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableClass", b =>
                {
                    b.Property<int>("PlayableClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayableClassId"), 1L, 1);

                    b.Property<int>("CharismaBonus")
                        .HasColumnType("int");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConstitutionBonus")
                        .HasColumnType("int");

                    b.Property<int>("DexterityBonus")
                        .HasColumnType("int");

                    b.Property<int>("IntelligenceBonus")
                        .HasColumnType("int");

                    b.Property<int>("StrengthBonus")
                        .HasColumnType("int");

                    b.Property<int>("WisdomBonus")
                        .HasColumnType("int");

                    b.HasKey("PlayableClassId");

                    b.ToTable("PlayableCharacterClasses");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableRace", b =>
                {
                    b.Property<int>("PlayableRaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayableRaceId"), 1L, 1);

                    b.Property<int>("AmountOfSkillsToChoose")
                        .HasColumnType("int");

                    b.Property<int>("CharismaBonus")
                        .HasColumnType("int");

                    b.Property<int>("ConstitutionBonus")
                        .HasColumnType("int");

                    b.Property<int>("DexterityBonus")
                        .HasColumnType("int");

                    b.Property<int>("IntelligenceBonus")
                        .HasColumnType("int");

                    b.Property<string>("RaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StrengthBonus")
                        .HasColumnType("int");

                    b.Property<int>("WisdomBonus")
                        .HasColumnType("int");

                    b.HasKey("PlayableRaceId");

                    b.ToTable("PlayableCharacterRaces");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.MetaTags.ActionCharacter", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterActionId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "CharacterActionId");

                    b.HasIndex("CharacterActionId");

                    b.ToTable("actionCharactersRelation");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.MetaTags.ItemCharacter", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("bit");

                    b.HasKey("ItemId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("ItemsCharactersRelation");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("ActiveStoryId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("DamageDealt")
                        .HasColumnType("int");

                    b.Property<int>("DamageReceived")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("LoggedIn")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoriesCompletedTotal")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Enemy", b =>
                {
                    b.HasBaseType("OstreCWEB.Data.Repository.Characters.CharacterModels.Character");

                    b.Property<int>("NonPlayableRace")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Enemy");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableCharacter", b =>
                {
                    b.HasBaseType("OstreCWEB.Data.Repository.Characters.CharacterModels.Character");

                    b.Property<int>("PlayableClassId")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("PlayableClassId");

                    b.HasIndex("RaceId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("PlayableCharacter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OstreCWEB.Data.Repository.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.CharacterAction", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.Status", "Status")
                        .WithMany("CharacterActions")
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Item", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.CharacterAction", "ActionToTrigger")
                        .WithMany("LinkedItems")
                        .HasForeignKey("ActionToTriggerCharacterActionId");

                    b.Navigation("ActionToTrigger");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.MetaTags.ActionCharacter", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.CharacterAction", "CharacterAction")
                        .WithMany("LinkedCharacter")
                        .HasForeignKey("CharacterActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.Character", "Character")
                        .WithMany("LinkedActions")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("CharacterAction");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.MetaTags.ItemCharacter", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.Character", "Character")
                        .WithMany("LinkedItems")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.Item", "Item")
                        .WithMany("LinkedCharacters")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableCharacter", b =>
                {
                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableClass", "CharacterClass")
                        .WithMany("PlayableCharacter")
                        .HasForeignKey("PlayableClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableRace", "Race")
                        .WithMany("PlayableCharacter")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OstreCWEB.Data.Repository.Identity.User", "User")
                        .WithMany("CharactersCreated")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterClass");

                    b.Navigation("Race");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Character", b =>
                {
                    b.Navigation("LinkedActions");

                    b.Navigation("LinkedItems");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.CharacterAction", b =>
                {
                    b.Navigation("LinkedCharacter");

                    b.Navigation("LinkedItems");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Item", b =>
                {
                    b.Navigation("LinkedCharacters");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableClass", b =>
                {
                    b.Navigation("PlayableCharacter");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.PlayableRace", b =>
                {
                    b.Navigation("PlayableCharacter");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Characters.CharacterModels.Status", b =>
                {
                    b.Navigation("CharacterActions");
                });

            modelBuilder.Entity("OstreCWEB.Data.Repository.Identity.User", b =>
                {
                    b.Navigation("CharactersCreated");
                });
#pragma warning restore 612, 618
        }
    }
}
