using IO.Swagger.Model;
using Klootzakken.Client.App;
using Klootzakken.Client.Data;
using Moq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Klootzakken.Client.Test.App
{
    public class ApiClientLobbyStatusServiceTest
    {
        private User user = new User();

        private LobbyView lobbyView_1 = new LobbyView();
        private LobbyView lobbyView_2 = new LobbyView();
        private LobbyView lobbyView_3 = new LobbyView();

        [Fact]
        public void mockedApiClient_getLobbies_returnsLobbies()
        {
            //ARRANGE
            var expectedLobbies = new List<LobbyView>
                    {
                        lobbyView_1,
                        lobbyView_2,
                        lobbyView_3
                    };

            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(client => client.GetAsync<List<LobbyView>>(It.IsAny<string>())).ReturnsAsync(expectedLobbies);
            var sut = new LobbyStatusService(mockedApiClient.Object);

            //ACT
            var lobbies = sut.GetLobbiesAsync().Result;

            //ASSERT
            lobbies.Should().BeAssignableTo<List<LobbyView>>();
            Assert.Equal(lobbies, expectedLobbies);
            //TODO: tests for throwing exception
        }

        [Fact]
        public void mockedApiClient_getMyLobbies_returnsMyLobbies()
        {
            //ARRANGE
            var expectedLobbies = new List<LobbyView>
                    {
                        lobbyView_1,
                        lobbyView_2,
                        lobbyView_3
                    };
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(client => client.GetAsync<List<LobbyView>>(It.IsAny<string>())).ReturnsAsync(expectedLobbies);
            var sut = new LobbyStatusService(mockedApiClient.Object);

            //ACT
            var lobbies = sut.GetMyLobbiesAsync().Result;

            //ASSERT
            lobbies.Should().BeAssignableTo<List<LobbyView>>();
            Assert.Equal(lobbies, expectedLobbies);
        }

        [Fact]
        public void mockedApiClient_getMyLobbies_returnsMyGames()
        {
            //ARRANGE
            var expectedGames = new List<LobbyView>
                    {
                        lobbyView_1,
                        lobbyView_2,
                        lobbyView_3
                    };
            var mockedApiClient = new Mock<IApiClient>();
            mockedApiClient.Setup(client => client.GetAsync<List<LobbyView>>(It.IsAny<string>())).ReturnsAsync(expectedGames);
            var sut = new LobbyStatusService(mockedApiClient.Object);

            //ACT
            var myGames = sut.GetMyGamesAsync().Result;

            //ASSERT
            myGames.Should().BeAssignableTo<List<LobbyView>>();
            Assert.Equal(myGames, expectedGames);
        }
    }
}
