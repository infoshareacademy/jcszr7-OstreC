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
    public class AddTests
    {
        [Fact]
        public void Add_WhenAddingValidFightInstance_ShouldReturnOperationSuccess()
        {
            // Arrange
            var repository = new FightRepository();
            int userId = 1;
            var fightInstance = new FightInstance();

            // Act
            var result = repository.Add(userId, fightInstance, out string operationResult);

            // Assert
            Assert.Equal("operation success", operationResult);
        }

        public void Add_WhenAddingNull_ShouldReturnOperationFailed()
        {
            // Arrange
            var repository = new FightRepository();
            int userId = 1;
            FightInstance fightInstance = null;

            // Act
            var result = repository.Add(userId, fightInstance, out string operationResult);

            // Assert
            Assert.Equal("operation failed", operationResult);
        }

        public void Add_WhenUserIdIsNegative_ShouldReturnOperationFailed()
        {
            // Arrange
            var repository = new FightRepository();
            int userId = -1;
            FightInstance fightInstance = null;

            // Act
            var result = repository.Add(userId, fightInstance, out string operationResult);

            // Assert
            Assert.Equal("operation failed", operationResult);
        }
    }
}
