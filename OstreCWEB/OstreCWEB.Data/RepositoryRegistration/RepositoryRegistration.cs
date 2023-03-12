using Microsoft.Extensions.DependencyInjection;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Characters;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.Identity;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Repository.Repository.StoryRepo;
using OstreCWEB.Repository.Repository.StoryRepo;

namespace OstreCWEB.Repository.RepositoryRegistration
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStoryRepository<Story>, StoryRepository>();
            services.AddTransient<IParagraphRepository<Paragraph>, ParagraphRepository>();

            services.AddTransient<IFightRepository, FightRepository>();

            services.AddTransient<ICharacterFactory, CharacterFactory>();
            services.AddTransient<IAbilitiesCharacterRepository, AbilitiesCharacterRepository>();
            services.AddTransient<IStatusRepository<Status>, StatusRepository>();
            services.AddTransient<IAbilitiesRepository<Ability>, AbilitiesRepository>();
            services.AddTransient<IPlayableCharacterRepository<PlayableCharacter>, PlayableCharacterRepository>();
            services.AddTransient<IIdentityRepository<User>, IdentityRepository>();
            services.AddTransient<IUserParagraphRepository<UserParagraph>, UserParagraphRepository>();
            services.AddTransient<IItemCharacterRepository<ItemCharacter>, ItemCharacterRepository>();
            services.AddTransient<IEnemyRepository<Enemy>, EnemyRepository>();
            services.AddTransient<IItemRepository<Item>, ItemRepository>();
            services.AddTransient<ICharacterClassRepository<PlayableClass>, CharacterClassRepository>();
            services.AddTransient<ICharacterRaceRepository<PlayableRace>, CharacterRaceRepository>();
        }
    }
}