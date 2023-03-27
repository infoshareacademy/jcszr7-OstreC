using FluentAssertions.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Moq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Services.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightRepositoryTests
{
    public class DeleteLinkedItemTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteLinkedItem_RemoveItemWithGivenId_ShouldReturnActualNumberOfItemsRemovesItems(int itemId)
        {
            // Arrange
            var repository = new FightRepository();
            var fightInstance = new FightInstance();
            var player = new PlayableCharacter();
            player.LinkedItems = new List<ItemCharacter>
            {
                new ItemCharacter { Id = 1 },
                new ItemCharacter { Id = 2 },
                new ItemCharacter { Id = 3 }
            };
            fightInstance.ActivePlayer = player;

            // Act
            repository.DeleteLinkedItem(fightInstance, itemId);

            // Assert
            Assert.Equal(2, fightInstance.ActivePlayer.LinkedItems.Count);
        }
    }
}
