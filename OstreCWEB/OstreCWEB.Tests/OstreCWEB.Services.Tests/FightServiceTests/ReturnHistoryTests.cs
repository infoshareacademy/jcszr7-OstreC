using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
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
    public class ReturnHistoryTests
    {

        public FightService _service;

        public ReturnHistoryTests()
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
        public void ReturnHistory_ReturnsFightHistory_WhenFightInstanceIsNotNull()
        {
            // Arrange
            var fightInstance = new FightInstance
            {
                FightHistory = new List<string> { "some text", "another text" }
            };

            // Act
            var result = _service.ReturnHistory(fightInstance);

            // Assert
            Assert.Equal(fightInstance.FightHistory, result);
        }

        [Fact]
        public void ReturnHistory_ReturnsNull_WhenFightInstanceIsNull()
        {
            // Arrange
            FightInstance fightInstance = null;

            // Act
            Func<List<string>> result = () => _service.ReturnHistory(fightInstance);

            // Assert
            result.Should().Throw<Exception>();
        }
    }
}
