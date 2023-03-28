using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class UpdateActiveTargetAsyncTests
    {
        public FightService _service;

        public UpdateActiveTargetAsyncTests()
        {

            var mockFightRepository = new Mock<IFightRepository>();
            var mockFightFactory = new Mock<IFightFactory>();
            var mockCharacterFactory = new Mock<ICharacterFactory>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserParagraphRepository = new Mock<IUserParagraphRepository<UserParagraph>>();
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<FightService>>();
            var mockGameService = new Mock<IGameService>();

            _service = new FightService(
                mockFightRepository.Object,
                mockFightFactory.Object,
                mockCharacterFactory.Object,
                mockHttpContextAccessor.Object,
                mockUserParagraphRepository.Object,
                mockUserService.Object,
                mockLogger.Object,
                mockGameService.Object
            );
        }
        [Fact]
        public async Task UpdateActiveTargetAsync_UpdateEnemy1ForActiveTarget_UpdatesActiveTargetCorrectly()
        {
            // Arrange
            var player = new PlayableCharacter { CombatId = 1 };
            var enemy1 = new Enemy { CombatId = 2 };
            var enemy2 = new Enemy { CombatId = 3 };
            var fightInstance = new FightInstance
            {
                ActivePlayer = player,
                ActiveEnemies = new List<Enemy> { enemy1, enemy2 }
            };


            // Act
            await _service.UpdateActiveTargetAsync(2, fightInstance);

            // Assert
            Assert.Equal(enemy1, fightInstance.ActiveTarget);
        }

        [Fact]
        public async Task UpdateActiveTargetAsync_Should_Set_ActivePlayer_As_ActiveTarget_When_Id_Matches_ActivePlayer_CombatId()
        {
            // Arrange
            var mockFightInstance = new FightInstance
            {
                ActivePlayer = new PlayableCharacter { CombatId = 1 },
                ActiveEnemies = new List<Enemy>
                {
            new Enemy { CombatId = 2 }
                }
            };

            var mockFightRepository = new Mock<IFightRepository>();


            // Act
            await _service.UpdateActiveTargetAsync(1, mockFightInstance);

            // Assert
            Assert.Equal(mockFightInstance.ActivePlayer, mockFightInstance.ActiveTarget);
        }

    }
}
