using Klootzakken.Client.App.GameApiService;
using Klootzakken.Client.Data;
using Moq;
using System.Net.Http;
using Xunit;
using FluentAssertions;

namespace Klootzakken.Client.Test.App.GameApiService
{
    public class ApiClientLobbyActionServiceTest
    {

        [Fact]
        public void mockedApiClient_createLobby_lobbyIsCreatedSuccesfully()
        {
            //ARRANGE
            var mockedApiCLient = new Mock<IApiClient>();
            mockedApiCLient.Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>())).ReturnsAsync(true);

            var sut = new LobbyActionService(mockedApiCLient.Object);

            //ACT
            var lobbyCreated = sut.CreateLobbyAsync(It.IsAny<string>()).Result;

            //ASSERT
            Assert.True(lobbyCreated);
        }
        //TODO: test for throwing exception (for the other service functions as well)

        [Fact]
        public void mockedApiClient_joinLobby_joinedToLobbySuccesfully()
        {
            //ARRANGE
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>())).ReturnsAsync(true);

            var sut = new LobbyActionService(mockedApiClient.Object);

            //ACT
            var joinedToLobby = sut.JoinLobbyAsync("daniLobbyId").Result;

            //ASSERT
            Assert.True(joinedToLobby);
        }

        [Fact]
        public void mockedApiClient_startGameForLobbyAsync_gameIsStartedSuccesfully()
        {
            //ARRANGE
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(client => client.PostAsync(It.IsAny<string>(), It.IsAny<StringContent>())).ReturnsAsync(true);

            var sut = new LobbyActionService(mockedApiClient.Object);

            //ACT
            var isGameStarted = sut.StartGameForLobbyAsync("daniLobbyId").Result;

            //ASSERT
            Assert.True(isGameStarted);
        }

    }
}

