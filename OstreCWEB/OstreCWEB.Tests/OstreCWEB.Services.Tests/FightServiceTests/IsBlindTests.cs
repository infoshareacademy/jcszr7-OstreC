using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using FluentAssertions;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class IsBlindTests
    {
        public FightService _service;

        public IsBlindTests()
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
         public void IsBlind_WithBlindStatus_ReturnsTrue()
        {
            // Arrange
            var character = new PlayableCharacter();
            character.ActiveStatuses.Add(new Status { StatusType = StatusType.Blind });

            // Act
           var result = _service.IsBlind(character);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBlind_WithBlessedStatus_ReturnsFalse()
        {
            // Arrange
            var character = new PlayableCharacter();
            character.ActiveStatuses.Add(new Status { StatusType = StatusType.Bless });

            // Act
            var result = _service.IsBlind(character);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBlind_WithMultipleStatuses_ReturnsTrue()
        {
            // Arrange
            var character = new PlayableCharacter();
            character.ActiveStatuses.Add(new Status { StatusType = StatusType.Bless });
            character.ActiveStatuses.Add(new Status { StatusType = StatusType.Blind });

            // Act
            var result = _service.IsBlind(character);

            // Assert
            Assert.True(result);
        }
    }
}
