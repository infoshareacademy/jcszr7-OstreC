using Microsoft.AspNetCore.Routing;
using Moq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.Repository.Repository.Fight;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightRepositoryTests
{
    public class DeleteTests
    {
        [Fact]
        public void Delete_WhenCalledWithValidUserIdAndCharacterId_RemovesFightInstanceAndReturnsTrue()
        {
            // Arrange
            var repository = new FightRepository();
            var fightInstance = new FightInstance();
            var player = new PlayableCharacter { Id = 1 };
            fightInstance.ActivePlayer = player;
            repository.Add(1, fightInstance, out _);

            // Act
            string operationResult;
            var result = repository.Delete(1, 1, out operationResult);

            // Assert
            Assert.True(result);
        }
    }
}