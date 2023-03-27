using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.CharacterModels.Enums;
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
    public class SpellCastingModifierTests
    {
        public FightService _service;
        public Character _character;
        public HttpContextAccessor _httpAccessor;

        public SpellCastingModifierTests()
        {

            var mockFightRepository = new Mock<IFightRepository>();
            var mockFightFactory = new Mock<IFightFactory>();
            var mockCharacterFactory = new Mock<ICharacterFactory>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserParagraphRepository = new Mock<IUserParagraphRepository<UserParagraph>>();
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<FightService>>();
            var mockGameService = new Mock<IGameService>();
            var mockCharacer = new Mock<Character>();

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
            _character = mockCharacer.Object;
        }


        [Theory]
        [InlineData(20,Statistics.Strenght)]
        [InlineData(18,Statistics.Strenght)]
        [InlineData(16,Statistics.Strenght)]
        [InlineData(12,Statistics.Strenght)]
        [InlineData(10,Statistics.Strenght)]
        [InlineData(20, Statistics.Constitution)]
        [InlineData(18, Statistics.Constitution)]
        [InlineData(16, Statistics.Constitution)]
        [InlineData(12, Statistics.Constitution)]
        [InlineData(10, Statistics.Constitution)]
        [InlineData(20, Statistics.Dexterity)]
        [InlineData(18, Statistics.Dexterity)]
        [InlineData(16, Statistics.Dexterity)]
        [InlineData(12, Statistics.Dexterity)]
        [InlineData(10, Statistics.Dexterity)]
        [InlineData(20, Statistics.Intelligence)]
        [InlineData(18, Statistics.Intelligence)]
        [InlineData(16, Statistics.Intelligence)]
        [InlineData(12, Statistics.Intelligence)]
        [InlineData(10, Statistics.Intelligence)]
        [InlineData(20, Statistics.Charisma)]
        [InlineData(18, Statistics.Charisma)]
        [InlineData(16, Statistics.Charisma)]
        [InlineData(12, Statistics.Charisma)]
        [InlineData(10, Statistics.Charisma)]
        [InlineData(20, Statistics.Wisdom)]
        [InlineData(18, Statistics.Wisdom)]
        [InlineData(16, Statistics.Wisdom)]
        [InlineData(12, Statistics.Wisdom)]
        [InlineData(10, Statistics.Wisdom)]
        public void SpellCastingModifier_ForEveryEnum_ReturnsCalculatedModifier(int value, Statistics statForTest)
        {
            //Arrange
            if (statForTest == Statistics.Strenght)
            {
                _character.Strenght = value;
            }

            if (statForTest == Statistics.Constitution)
            {
                _character.Constitution = value;
            }

            if (statForTest == Statistics.Dexterity)
            {
                _character.Dexterity = value;
            }

            if (statForTest == Statistics.Intelligence)
            {
                _character.Intelligence = value;
            }

            if (statForTest == Statistics.Charisma)
            {
                _character.Charisma = value;
            }

            if (statForTest == Statistics.Wisdom)
            {
                _character.Wisdom = value;
            }

            var expectedModifier = 8 + _service.CalculateModifier(value);

            // Act
            var result = _service.SpellCastingModifier(_character, statForTest);

            // Assert
            result.Should().Be(expectedModifier);

            //Assert.Equal(result,12);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(10000000)]
        public void SpellCastingModifier_ValueOutOfRange_ThrowsException(int value)
        {
            //Arrange
            _character.Strenght = value;
            // Act
            Func<int> result = () => _service.CalculateModifier(_character.Strenght);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
