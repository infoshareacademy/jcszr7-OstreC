using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class UpdateFightHistoryTests
    {

        public FightService _service;

        public UpdateFightHistoryTests()
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

        [Theory]
        [InlineData("Now tell me what you want")]
        [InlineData("What you really, really want?")]
        [InlineData("I Wanna, wanna")]
        public void UpdateFightHistory_AddMessageToList_ReturnUpdatedList(string message)
        {
            // Arrange
            var history = new List<string> { "First message" };

            // Act
            _service.UpdateFightHistory(history, message);

            // Assert
            Assert.Equal(2,(history.Count));
        }
    }
}
