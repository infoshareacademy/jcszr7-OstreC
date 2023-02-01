﻿using Microsoft.Extensions.DependencyInjection;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Fight;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.ManyToMany;
using OstreCWEB.Data.Repository.StoryModels;
using OstreCWEB.Data.Repository.SuperAdmin;  
using OstreCWEB.Data.Factory;

namespace OstreCWEB.Data.RepositoryRegistration
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStoryRepository, StoryRepository>();
            services.AddTransient<IFightRepository, FightRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<ICharacterActionsRepository, CharacterActionRepository>();
            services.AddTransient<IPlayableCharacterRepository, PlayableCharacterRepository>();
            services.AddTransient<ISuperAdminRepository, SuperAdminRepository>();
            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<IUserParagraphRepository, UserParagraphRepository>();
            services.AddTransient<IItemCharacterRepository, ItemCharacterRepository>();
            services.AddTransient<IActionCharacterRepository, ActionCharacterRepository>();
            services.AddTransient<ICharacterFactory, CharacterFactory>();
            services.AddTransient<IEnemyRepository, EnemyRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<ICharacterClassRepository, CharacterClassRepository>();
            services.AddTransient<ICharacterRaceRepository, CharacterRaceRepository>();
        }
    }
}
