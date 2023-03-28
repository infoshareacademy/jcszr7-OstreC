using Microsoft.AspNetCore.Routing;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.Services.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class ResetActiveTargetTests
    {
        [Fact]
        public void ResetActiveTarget_GivenProperValue_ShouldReturnNewActiveTarget()
        {
            // Arrange
            var fightInstance = new FightInstance();
            var expectedActiveTarget = new PlayableCharacter();

            var fightService = new FightService(null, null, null, null, null, null, null, null);

            // Act
            var result = fightService.ResetActiveTarget(fightInstance);

            // Assert
            Assert.Equal(expectedActiveTarget.GetType(), result.GetType());
            Assert.Equal(expectedActiveTarget.CombatId, result.CombatId);

        }
    }
}
