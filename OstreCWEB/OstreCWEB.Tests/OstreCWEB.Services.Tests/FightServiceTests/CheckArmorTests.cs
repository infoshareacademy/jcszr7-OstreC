using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
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
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class CheckArmorTests
    {
        public FightService _service;

        public CheckArmorTests()
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
        public void CheckArmor_WhenNoArmorIsEquipped_ReturnsArmorClassZero()
        {
            // Arrange
            Character target = new PlayableCharacter();

            // Act
            int armorClass = _service.CheckArmor(target);

            // Assert
            Assert.Equal(0, armorClass);
        }

        [Fact]
        public void CheckArmor_WhenArmorWith14IsEquipped_ReturnsArmorClass14()
        {
            // Arrange
            var armor = new Item { ItemType = ItemType.Armor, ArmorClass = 14, Name = "Chainmail" };
            var linkedItem = new ItemCharacter { Item = armor, IsEquipped = true };
            var target = new PlayableCharacter { LinkedItems = new List<ItemCharacter> { linkedItem } };

            var fakeRepo = new Mock<IItemCharacterRepository<ItemCharacter>>();
            fakeRepo.Setup(r => r.GetAll()).Returns(new List<ItemCharacter> { linkedItem }.ToList());

            // Act
            int armorClass = _service.CheckArmor(target);

            // Assert
            Assert.Equal(14, armorClass);
        }

        [Theory]
        [InlineData(ItemType.Armor, 14, 14)]
        [InlineData(ItemType.Shield, 5, 5)]
        [InlineData(ItemType.Armor, 0, 0)]
        public void CheckArmor_WhenDifferentItemsEquipped_ReturnsCorrectArmorClass(
        ItemType itemType, int armorClass, int expectedArmorClass)
        {
            // Arrange
            var item = new Item { ItemType = itemType, ArmorClass = armorClass };
            var linkedItem = new ItemCharacter { Item = item, IsEquipped = true };
            var target = new PlayableCharacter { LinkedItems = new List<ItemCharacter> { linkedItem } };

            var fakeRepo = new Mock<IItemCharacterRepository<ItemCharacter>>();
            fakeRepo.Setup(r => r.GetAll()).Returns(new List<ItemCharacter> { linkedItem }.ToList());

            // Act
            int actualArmorClass = _service.CheckArmor(target);

            // Assert
            Assert.Equal(expectedArmorClass, actualArmorClass);
        }
    }
}
