using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.DomainModels.StoryModels.Properties;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class InitializeFightAsyncTests
    {
        private readonly FightService _service;

        public InitializeFightAsyncTests()
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


        //[Fact]
        //public async Task InitializeFightAsync_ShouldAddNewFightInstanceToRepositoryAndReturnIt()
        //{
        //    // Arrange
        //    int userId = 1;
        //    UserParagraph gameInstance = new UserParagraph
        //    {
        //        ActiveCharacter = new PlayableCharacter(),
        //        ActiveCharacterId = 1,
        //        User = new User(), // Set up User instance
        //        Paragraph = new Paragraph(), // Set up Paragraph instance
        //        ActiveGame = true // Set up ActiveGame property
        //    };
        //    FightInstance expectedFightInstance = new FightInstance
        //    {
        //        ActiveEnemies = new List<Enemy>(),
        //        TurnNumber = 1,
        //        PlayerActionCounter = 0,
        //        CombatFinished = false,
        //        PlayerWon = false,
        //        ItemToDeleteId = 0,
        //        IsItemToDelete = false,
        //        ActionGrantedByItem = false,
        //        UserParagraphId = gameInstance.Id,
        //        isPlayerFirst = true,
        //        AiFirstTurnCounter = 1
        //    };
        //    var mockFightFactory = new Mock<IFightFactory>();
        //    mockFightFactory.Setup(f => f.BuildNewFightInstance(gameInstance, It.IsAny<List<Enemy>>()))
        //                    .Returns(expectedFightInstance);

        //    var mockCharacterFactory = new Mock<ICharacterFactory>();
        //    mockCharacterFactory.Setup(c => c.CreateEnemiesInstances(It.IsAny<List<EnemyInParagraph>>()))
        //                        .ReturnsAsync(new List<Enemy>());
        //    var mockFightRepository = new Mock<IFightRepository>();
        //    string operationResult;

        //    // Act
        //    var result = await _service.InitializeFightAsync(userId, gameInstance);

        //    // Assert
        //    mockFightFactory.Verify(f => f.BuildNewFightInstance(gameInstance, It.IsAny<List<Enemy>>()), Times.Once);
        //    mockFightRepository.Verify(r => r.Add(userId, expectedFightInstance, out operationResult), Times.Once);
        //    Assert.Equal(expectedFightInstance, result);
        //}

    }
}

