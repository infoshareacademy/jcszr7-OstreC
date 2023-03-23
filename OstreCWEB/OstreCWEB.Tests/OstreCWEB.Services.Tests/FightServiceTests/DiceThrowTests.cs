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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class DiceThrowTests
    {
        public FightService _service;

        public DiceThrowTests()
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
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(100)]
        public void DiceThrow_For1000Throws_ReturnsValuesInRange(int maxValue)
        {
            int numberOfTests = 1000;

            for (int i = 0; i < numberOfTests; i++)
            {
                var result = _service.DiceThrow(maxValue);

                Assert.InRange(result, 1, maxValue);
            }

        }

    }
}
