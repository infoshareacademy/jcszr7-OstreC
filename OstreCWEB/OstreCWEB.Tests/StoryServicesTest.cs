using AutoMapper;
using FluentAssertions;
using Moq;
using OstreCWEB.DomainModels.CharacterModels;
using OstreCWEB.DomainModels.StoryModels;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.Repository.Repository.StoryRepo;
using OstreCWEB.Services.StoryService;
using OstreCWEB.Services.StoryService.ModelsDto;

namespace OstreCWEB.Tests
{
    public class StoryServicesTest
    {
        private Mock<IMapper> _mapperMock;

        private Mock<IStoryRepository<Story>> _storyRepositoryMock;
        private Mock<IEnemyRepository<Enemy>> _enemyRepositoryMock;
        private Mock<IItemRepository<Item>> _itemRepositoryMock;

        private StoryServices _storyService;

        public StoryServicesTest()
        {
            _mapperMock = new Mock<IMapper>();

            _storyRepositoryMock = new Mock<IStoryRepository<Story>>();
            _enemyRepositoryMock = new Mock<IEnemyRepository<Enemy>>();
            _itemRepositoryMock = new Mock<IItemRepository<Item>>();

            _storyService = new StoryServices(_storyRepositoryMock.Object, _enemyRepositoryMock.Object, _itemRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Exists_StoryDoesntExist_ReturnsFalse()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.Exists(1))
                .Returns(false);

            // Act
            var result = _storyService.Exists(1);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Exists_StoryExist_ReturnsTrue()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.Exists(1))
                .Returns(true);

            // Act
            var result = _storyService.Exists(1);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllStories_CallsRepositoryGetAllStories()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.GetAllStories())
                .ReturnsAsync(new List<Story>());

            // Act
            var result = await _storyService.GetAllStories();

            // Assert
            _storyRepositoryMock.Verify(x => x.GetAllStories(), Times.Once());
        }

        [Fact]
        public async Task GetAllStories_ResturnsListStoryView()
        {
            // Arrange
            var stories = new List<Story>
                {
                    new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 },
                    new Story { Id = 2, Description = "Test2", Name = "Test2", UserId = 1 }
                };

            _storyRepositoryMock
                .Setup(x => x.GetAllStories())
                .ReturnsAsync(stories);

            var expected = new List<StoryView>
            {
                new StoryView { Id = 1, Description = "Test", Name = "Test" },
                new StoryView { Id = 2, Description = "Test2", Name = "Test2" }
            };

            _mapperMock
                .Setup(x => x.Map<List<StoryView>>(stories))
                .Returns(expected);

            // Act
            var result = await _storyService.GetAllStories();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetStoriesByUserId_CallsRepositoryGetStoriesByUserId()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.GetStoriesByUserId(1))
                .ReturnsAsync(new List<Story>());

            // Act
            var result = await _storyService.GetStoriesByUserId(1);

            // Assert
            _storyRepositoryMock.Verify(x => x.GetStoriesByUserId(1), Times.Once());
        }

        [Fact]
        public async Task GetStoriesByUserId_ResturnsListStoryView()
        {
            // Arrange
            var stories = new List<Story>
                {
                    new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 },
                    new Story { Id = 2, Description = "Test2", Name = "Test2", UserId = 1 }
                };

            _storyRepositoryMock
                .Setup(x => x.GetStoriesByUserId(1))
                .ReturnsAsync(stories);

            var expected = new List<StoryView>
            {
                new StoryView { Id = 1, Description = "Test", Name = "Test" },
                new StoryView { Id = 2, Description = "Test2", Name = "Test2" }
            };

            _mapperMock
                .Setup(x => x.Map<List<StoryView>>(stories))
                .Returns(expected);

            // Act
            var result = await _storyService.GetStoriesByUserId(1);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetStoryWithParagraphsByIdAsync_CallsRepositoryGetStoryWithParagraphsByIdAsync()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.GetStoryWithParagraphsByIdAsync(1))
                .ReturnsAsync(new Story());

            // Act
            var result = await _storyService.GetStoryWithParagraphsByIdAsync(1);

            // Assert
            _storyRepositoryMock.Verify(x => x.GetStoryWithParagraphsByIdAsync(1), Times.Once());
        }

        [Fact]
        public async Task GetStoryWithParagraphsByIdAsync_ResturnsStoryView()
        {
            // Arrange
            var story = new Story { Id = 2, Description = "Test2", Name = "Test2", UserId = 1 };

            _storyRepositoryMock
                .Setup(x => x.GetStoryWithParagraphsByIdAsync(1))
                .ReturnsAsync(story);

            var expected = new StoryView { Id = 1, Description = "Test", Name = "Test" };

            _mapperMock
                .Setup(x => x.Map<StoryView>(story))
                .Returns(expected);

            // Act
            var result = await _storyService.GetStoryWithParagraphsByIdAsync(1);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetParagraphsByIdStoryAsync_CallsRepositoryGetStoryWithParagraphsByIdAsync()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.GetStoryWithParagraphsByIdAsync(1))
                .ReturnsAsync(new Story());

            // Act
            var result = await _storyService.GetParagraphsByIdStoryAsync(1);

            // Assert
            _storyRepositoryMock.Verify(x => x.GetStoryWithParagraphsByIdAsync(1), Times.Once());
        }

        [Fact]
        public async Task GetParagraphsByIdStoryAsync_ResturnsStoryView()
        {
            // Arrange
            var story = new Story { Id = 2, Description = "Test2", Name = "Test2", UserId = 1 };

            _storyRepositoryMock
                .Setup(x => x.GetStoryWithParagraphsByIdAsync(1))
                .ReturnsAsync(story);

            var expected = new StoryParagraphsView { Id = 1, Description = "Test", Name = "Test" };

            _mapperMock
                .Setup(x => x.Map<StoryParagraphsView>(story))
                .Returns(expected);

            // Act
            var result = await _storyService.GetParagraphsByIdStoryAsync(1);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddStory_CallsRepositoryAddStory()
        {
            // Arrange
            var story = new StoryView { Description = "Test2", Name = "Test2" };

            var expected = new Story { Description = "Test2", Name = "Test2" };

            _mapperMock
                .Setup(x => x.Map<Story>(story))
                .Returns(expected);

            // Act
            await _storyService.AddStory(story, 1);

            // Assert
            _storyRepositoryMock.Verify(x => x.AddStory(expected), Times.Once());
        }

        [Fact]
        public async Task GetStoryByIdAsync_CallsRepositoryGetStoryByIdAsync()
        {
            // Arrange
            _storyRepositoryMock
                .Setup(x => x.GetStoriesByUserId(1))
                .ReturnsAsync(new List<Story>());

            // Act
            var result = await _storyService.GetStoryByIdAsync(1);

            // Assert
            _storyRepositoryMock.Verify(x => x.GetStoryByIdAsync(1), Times.Once());
        }

        [Fact]
        public async Task GetStoryByIdAsync_ResturnsStoryView()
        {
            // Arrange
            var story = new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 };

            _storyRepositoryMock
                .Setup(x => x.GetStoryByIdAsync(1))
                .ReturnsAsync(story);

            var expected = new StoryView { Id = 1, Description = "Test", Name = "Test" };

            _mapperMock
                .Setup(x => x.Map<StoryView>(story))
                .Returns(expected);

            // Act
            var result = await _storyService.GetStoryByIdAsync(1);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task UpdateStory_UserIsOwner_CallsRepositoryUpdateStory()
        {
            // Arrange
            var story = new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 };
            _storyRepositoryMock
                .Setup(x => x.GetStoryByIdAsync(1))
                .ReturnsAsync(story);

            _storyRepositoryMock
                .Setup(x => x.Exists(1))
                .Returns(true);

            var uStory = new StoryView { Id = 1, Description = "Test - Change", Name = "Test - Change" };

            // Act
            _storyService.UpdateStory(uStory, 1);

            // Assert
            _storyRepositoryMock.Verify(x => x.UpdateStory(story), Times.Once());
        }

        [Fact]
        public async Task UpdateStory_UserIsNotOwner_ThrowsException()
        {
            // Arrange
            var story = new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 };
            _storyRepositoryMock
                .Setup(x => x.GetStoryByIdAsync(1))
                .ReturnsAsync(story);

            _storyRepositoryMock
                .Setup(x => x.Exists(1))
                .Returns(true);

            var uStory = new StoryView { Id = 1, Description = "Test - Change", Name = "Test - Change" };

            // Act
            Action resultWithInvalidUserId = () => _storyService.UpdateStory(uStory, 2);

            // Assert
            resultWithInvalidUserId.Should().Throw<Exception>("because userId is invalid")
                .WithMessage("This is not your Story");
        }

        [Fact]
        public async Task UpdateStory_StoryDoesntExist_ThrowsException()
        {
            // Arrange
            var story = new Story { Id = 1, Description = "Test", Name = "Test", UserId = 1 };
            _storyRepositoryMock
                .Setup(x => x.Exists(1))
                .Returns(false);

            var uStory = new StoryView { Id = 1, Description = "Test - Change", Name = "Test - Change" };

            // Act
            Action resultWithInvalidUserId = () => _storyService.UpdateStory(uStory, 1);

            // Assert
            resultWithInvalidUserId.Should().Throw<Exception>("because story doesn't exist")
                .WithMessage("Story doesn't exist");
        }
    }
}