using FluentAssertions;
using Moq; 
using OstreCWEB.Services.Api;
using OstreCWEB.ViewModel.Api;

namespace OstreCWEB.Tests
{
    public class FithEditionClientApiTest
    {
        private Mock<IHttpClientFactory> _httpClientFactoryMock;
        private Mock<Filter> _filter;
        public FithEditionClientApiTest()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>(); 
        }
        [Fact]
        public async Task Test1()
        {
            //Arrange
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient() { BaseAddress = new Uri("https://api.open5e.com") });
            var x = new FithEditionApiClient(_httpClientFactoryMock.Object); 
            //Act
            var y = await x.GetSpells(new Filter() {},0);
            //Assert
            y.Count.Should().NotBe(0);
        }
        [Fact]
        public async Task Test2()
        {
            //Arrange
            _httpClientFactoryMock.Setup(x => x.CreateClient("FithEditionApiClient")).Returns(new HttpClient() { BaseAddress = new Uri("https://api.open5e.com") });
            var x = new FithEditionApiClient(_httpClientFactoryMock.Object);
            //Act
            var y = await x.GetSpells(new Filter(){},0);
            //Assert
            y.Count.Should().NotBe(0);
        }
        [Fact]
        public async Task Test3()
        {
            //Arrange
            _httpClientFactoryMock.Setup(x => x.CreateClient("?")).Returns(new HttpClient() { BaseAddress = new Uri("https://api.open5e.com") });
            var x = new FithEditionApiClient(_httpClientFactoryMock.Object);
            //Act
            Func<Task> y = ()=> x.GetSpells(new Filter(){},0);

            //Assert
            await y.Should().ThrowAsync<Exception>();
            
        }

    }
}