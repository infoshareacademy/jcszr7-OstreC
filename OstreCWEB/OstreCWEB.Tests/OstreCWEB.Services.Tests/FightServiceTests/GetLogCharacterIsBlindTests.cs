using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.CharacterModels;
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
    public class GetLogCharacterIsBlindTests
    {
        private readonly FightService _service;

        public GetLogCharacterIsBlindTests()
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
        public void GetLogCharacterIsBlind_WithBlindCharacter_ReturnsAttackWithDisadvantageLog()
        {
            // Arrange
            var character = new PlayableCharacter();
            character.ActiveStatuses.Add(new Status { StatusType = StatusType.Blind });

            // Act
            var result = _service.GetLogCharacterIsBlind(character);

            // Assert
            Assert.Equal("attack was made with disadvantage due to blind status", result);
        }

        [Fact]
        public void GetLogCharacterIsBlind_WithNonBlindCharacter_ReturnsEmptyString()
        {
            // Arrange
            var character = new PlayableCharacter();

            // Act
            var result = _service.GetLogCharacterIsBlind(character);

            // Assert
            Assert.Equal("", result);
        }
    }

}
