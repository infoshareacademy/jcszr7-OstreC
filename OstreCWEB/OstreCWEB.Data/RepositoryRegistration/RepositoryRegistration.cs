using Microsoft.Extensions.DependencyInjection;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Repository.Repository.StoryModels;
using OstreCWEB.Repository.Repository.SuperAdmin;

namespace OstreCWEB.Repository.RepositoryRegistration
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStoryRepository, StoryRepository>();
            services.AddTransient<IFightRepository, FightRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IAbilitiesRepository, AbilitiesRepository>();
            services.AddTransient<IPlayableCharacterRepository, PlayableCharacterRepository>();
            services.AddTransient<ISuperAdminRepository, SuperAdminRepository>();
            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<IUserParagraphRepository<UserParagraph>, UserParagraphRepository>();
            services.AddTransient<IItemCharacterRepository, ItemCharacterRepository>();
            services.AddTransient<IAbilitiesCharacterRepository, AbilitiesCharacterRepository>();
            services.AddTransient<ICharacterFactory, CharacterFactory>();
            services.AddTransient<IEnemyRepository, EnemyRepository>();
            services.AddTransient<IItemRepository<Item>, ItemRepository>();
            services.AddTransient<ICharacterClassRepository, CharacterClassRepository>();
            services.AddTransient<ICharacterRaceRepository, CharacterRaceRepository>();
        }
    }
}
