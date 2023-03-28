using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
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
    public class ApplyDamageTests
    {
        private readonly FightService _service;

        public ApplyDamageTests()
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
        public void ApplyDamage_WithOneDamageWithoutSavingThrow_ReturnsReducedHealthPoints()
        {
            // Arrange
            var target = new PlayableCharacter { CurrentHealthPoints = 20 };
            var actions = new Ability { Max_Dmg = 1, Hit_Dice_Nr = 1 };

            // Act
            var actualUpdatedValue = _service.ApplyDamage(target, actions, false);
            // Assert
            Assert.Equal(target.CurrentHealthPoints, 19);
        }


        [Fact]
        public void ApplyDamage_WithOneDamageWithSavingThrow_ReturnsReducedHealthPoints()
        {
            // Arrange
            var target = new PlayableCharacter { CurrentHealthPoints = 20 };
            var actions = new Ability { Max_Dmg = 1, Hit_Dice_Nr = 2 };

            // Act
            var actualUpdatedValue = _service.ApplyDamage(target, actions, true);
            // Assert
            Assert.Equal(target.CurrentHealthPoints, 18);
        }

    }

}
