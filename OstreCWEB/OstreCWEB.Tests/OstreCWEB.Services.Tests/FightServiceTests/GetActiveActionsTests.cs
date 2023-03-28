using FluentAssertions;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class GetActiveActionsTests
    {

        public FightService _service;
        public FightInstance _instance;

        public GetActiveActionsTests()
        {
            var mockFightInstance = new Mock<FightInstance>();
            var mockFightRepository = new Mock<IFightRepository>();
            var mockFightFactory = new Mock<IFightFactory>();
            var mockCharacterFactory = new Mock<ICharacterFactory>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserParagraphRepository = new Mock<IUserParagraphRepository<UserParagraph>>();
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<FightService>>();
            var mockGameService = new Mock<IGameService>();

            _instance = mockFightInstance.Object;
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
        public void GetActiveActions_WhenActiveActionsIsNull_ReturnsNull()
        {
            // Arrange
            _instance.ActiveAction = null;

            // Act
            var activeActions = _service.GetActiveActions(_instance);

            // Assert
            Assert.Null(activeActions);
        }

        [Fact]
        public void GetActiveActions_WhenActiveActionsIsNotNull_ReturnsActiveActions()
        {
            // Arrange
            var expectedActions = new Ability();
            _instance.ActiveAction = expectedActions;

            // Act
            var actualActions = _service.GetActiveActions(_instance);

            // Assert
            Assert.Equal(expectedActions, actualActions);
        }

    }
}
