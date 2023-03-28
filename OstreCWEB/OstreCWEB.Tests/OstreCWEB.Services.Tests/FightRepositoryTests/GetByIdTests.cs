using FluentAssertions;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.Repository.Repository.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightRepositoryTests
{
    public class GetByIdTests
    {


        [Theory]
        [InlineData(10, 10)]
        [InlineData(20, 20)]
        public void GetById_WhenProvidedValidUserIdAndCharacterId_ShouldReturnFightInstance(int userId, int playableCharacterId)
        {
            // Arrange
            var repository = new FightRepository();
            var fightInstance = new FightInstance();
            fightInstance.ActivePlayer = new PlayableCharacter() { Id = playableCharacterId };

            repository.Add(userId, fightInstance, out string operationResult);

            // Act
            var result = repository.GetById(userId, playableCharacterId);

            // Assert
            result.Should().Be(fightInstance);
        }


        [Fact]
        public void GetById_WhenProvidedInvalidUserIdOrCharacterId_ShouldReturnNull_()
        {
            // Arrange
            var repository = new FightRepository();
            int userId = 1;
            var fightInstance = new FightInstance();
            fightInstance.ActivePlayer = new PlayableCharacter() { Id = 1 };

            repository.Add(userId, fightInstance, out string operationResult);

            // Act
            var invalidUserIdResult = repository.GetById(2, 1);

            // Assert
            invalidUserIdResult.Should().Be(null);
            //invalidCharacterIdResult.Should.Be(null);

        }
    }
}

