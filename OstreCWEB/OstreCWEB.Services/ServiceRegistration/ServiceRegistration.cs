using Microsoft.Extensions.DependencyInjection;
using OstreCWEB.Services.Api;
using OstreCWEB.Services.Characters;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity; 
using OstreCWEB.Services.StoryServices;

namespace OstreCWEB.Services.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStoryService, StoryService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFightService, FightService>();
            services.AddTransient<IPlayableCharacterService, PlayableCharacterService>();
            services.AddTransient<IGameService, GameService>(); 
            services.AddTransient<IFightFactory, FightFactory>();
            services.AddTransient<IFithEditionApiClient, FithEditionApiClient>();
        }
    }
}
