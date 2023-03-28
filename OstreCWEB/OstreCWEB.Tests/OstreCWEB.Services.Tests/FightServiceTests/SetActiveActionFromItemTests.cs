using Moq;
using OstreCWEB.DomainModels.CharacterModels.Enums;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.DomainModels.ManyToMany;
using OstreCWEB.Repository.Repository.ManyToMany;
using OstreCWEB.Services.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OstreCWEB.Repository.Factory;
using OstreCWEB.Repository.Repository.Fight;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class SetActiveActionFromItemTests
    {
        public FightService _service;

        public SetActiveActionFromItemTests()
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
        public async Task SetActiveActionFromItem_SetsActiveActionAndItemToDelete_WhenChosenItemHasDeleteOnUseReturnsTrue()
        {
            // Arrange
            var fightInstance = new FightInstance();
            var item = new Item { ItemType = ItemType.SpecialItem, DeleteOnUse = true };
            var itemCharacter = new ItemCharacter { Id = 1, Item = item };
            var player = new PlayableCharacter { LinkedItems = new List<ItemCharacter> { itemCharacter } };
            fightInstance.ActivePlayer = player;
            var id = itemCharacter.Id;

            var fakeRepo = new Mock<IItemCharacterRepository<ItemCharacter>>();
            fakeRepo.Setup(r => r.GetAll()).Returns(new List<ItemCharacter> { itemCharacter }.ToList());

            // Act
            _service.SetActiveActionFromItem(fightInstance, id);

            // Assert
            Assert.True(fightInstance.ActionGrantedByItem);
            Assert.True(fightInstance.IsItemToDelete);
            Assert.Equal(id, fightInstance.ItemToDeleteId);
            Assert.Null(fightInstance.ActiveTarget);
        }
    }
}
