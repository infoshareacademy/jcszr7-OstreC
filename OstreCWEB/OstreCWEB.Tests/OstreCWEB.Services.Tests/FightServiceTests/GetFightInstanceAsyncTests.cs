using FluentAssertions;
using Moq;
using OstreCWEB.DomainModels.Fight;
using OstreCWEB.Services.Fight;
using Xunit;

namespace OstreCWEB.Tests.OstreCWEB.Services.Tests.FightServiceTests
{
    public class GetFightInstanceAsyncTests
    {
        //    private Mock<IFightInstance> _fightInstanceMock;
        //private Mock<IFightRepository> _fightRepositoryMock;
        //private Mock<IFightFactory> _fightFactoryMock;
        //private Mock<ICharacterFactory> _characterFactoryMock;
        //private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        //private Mock<IUserParagraphRepository<UserParagraph>> _userParagraphRepositoryMock;
        //private Mock<IUserService> _userServiceMock;
        //private Mock<ILogger<FightService>> _loggerMock;
        //private Mock<IGameService> _gameServiceMock;

        //private FightService _fightService;
        //public GetFightInstanceAsyncTests()
        //{
        //    _fightRepositoryMock = new Mock<IFightRepository>();
        //    _fightFactoryMock = new Mock<IFightFactory>();
        //    _characterFactoryMock = new Mock<ICharacterFactory>();
        //    _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        //    _userParagraphRepositoryMock = new Mock<IUserParagraphRepository<UserParagraph>>();
        //    _userServiceMock = new Mock<IUserService>();
        //    _loggerMock = new Mock<ILogger<FightService>>();
        //    _gameServiceMock = new Mock<IGameService>();
        //        _fightInstanceMock = new Mock<IFightInstance>();
        //    _fightService = new FightService(
        //        _fightRepositoryMock.Object,
        //        _fightFactoryMock.Object,
        //        _characterFactoryMock.Object,
        //        _httpContextAccessorMock.Object,
        //        _userParagraphRepositoryMock.Object,
        //        _userServiceMock.Object,
        //        _loggerMock.Object,
        //        _gameServiceMock.Object);

        //[Fact]
        //public async Task GetFightInstanceAsync_ForProperValues_ReturnsCorrectFightInstance()
        //{
        //    Arrange
        //   var fightServiceMock = new Mock<FightService>();
        //    var fightInstanceMock = new Mock<FightInstance>();
        //    var result = fightServiceMock.Setup(m => m.DiceThrow(20)).Returns(10);

        //    var userId = 1;
        //    var characterId = 1;

        //    var userParagraph = 1;
        //    var fightInstance = new FightInstance { ActivePlayer = new PlayableCharacter { Id = characterId } };
        //    _fightRepositoryMock.Setup(m => m.GetById(userId, characterId)).Returns(fightInstance);

        //    // Act
        //    var result = await _fightService.GetFightInstanceAsync();

        //    // Assert
        //    result.Should().Be(fightInstanceMock);
        //}
    }
}

