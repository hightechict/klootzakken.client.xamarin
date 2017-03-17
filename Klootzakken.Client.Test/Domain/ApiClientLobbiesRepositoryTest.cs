using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Klootzakken.Client.Domain;
using Klootzakken.Client.Data;
using IO.Swagger.Model;

namespace Klootzakken.Client.Test.Domain
{
    public class ApiClientLobbiesRepositoryTest
    {
        private Mock<IApiClient> _mockedApiClient = new Mock<IApiClient>();
        /*
        [Fact]
        public void mockedApiClient_getLobby_returnsLobby()
        {
            //TODO: change to LobbyView
            //ARRANGE
            var expectedLobby = new Lobby {Id="1", Name="lobby1"};

            _mockedApiClient.Setup(client => client.GetAsync<Lobby>(It.IsAny<string>()))
                .ReturnsAsync(expectedLobby);

            var sut = new ApiClientLobbiesRepository(_mockedApiClient.Object); 

            //ACT
            var lobby = sut.GetLobby().Result;

            //ASSERT
            lobby.Should().BeAssignableTo<Lobby>()
                .And.BeSameAs(expectedLobby);
            //TODO: check doc

        }

        [Fact]
        public void mockedApiClient_getMyLobbies_returnsMyLobbies()
        {
            //ARRANGE
            var expectedLobby_1 = new Lobby { Id = "1", Name = "lobby1" };
            var expectedLobby_2 = new Lobby { Id = "2", Name = "lobby2" };

            _mockedApiClient.Setup(client => client.GetAsync<List<Lobby>>(It.IsAny<string>()))
                .ReturnsAsync(new List<Lobby> {expectedLobby_1, expectedLobby_2 });

            var sut = new ApiClientLobbiesRepository(_mockedApiClient.Object);

            //ACT
            var myLobbies = sut.GetMyLobbies().Result;

            //ASSERT
            myLobbies.Should().BeAssignableTo<List<Lobby>>()
                .And.Equal(new List<Lobby> { expectedLobby_1, expectedLobby_2 });
            //TODO: check doc
        }

        [Fact]
        public void mockedApiClient_getLobbies_returnsLobbies()
        {
            //ARRANGE
            var expectedLobby_1 = new Lobby { Id = "1", Name = "lobby1" };
            var expectedLobby_2 = new Lobby { Id = "2", Name = "lobby2" };

            _mockedApiClient.Setup(client => client.GetAsync<List<Lobby>>(It.IsAny<string>()))
                .ReturnsAsync(new List<Lobby> { expectedLobby_1, expectedLobby_2 });

            var sut = new ApiClientLobbiesRepository(_mockedApiClient.Object);

            //ACT
            var myLobbies = sut.GetLobbies().Result;

            //ASSERT
            myLobbies.Should().BeAssignableTo<List<Lobby>>()
                .And.Equal(new List<Lobby> { expectedLobby_1, expectedLobby_2 });
            //TODO: check doc
        }

        [Fact]
        public void mockedApiClient_getMyGames_returnsMyGames()
        {
            //ARRANGE
            var expectedLobby_1 = new Lobby { Id = "1", Name = "lobby1" };
            var expectedLobby_2 = new Lobby { Id = "2", Name = "lobby2" };

            _mockedApiClient.Setup(client => client.GetAsync<List<Lobby>>(It.IsAny<string>()))
                .ReturnsAsync(new List<Lobby> { expectedLobby_1, expectedLobby_2 });

            var sut = new ApiClientLobbiesRepository(_mockedApiClient.Object);

            //ACT
            var myLobbies = sut.GetMyGames().Result;

            //ASSERT
            myLobbies.Should().BeAssignableTo<List<Lobby>>()
                .And.Equal(new List<Lobby> { expectedLobby_1, expectedLobby_2 });
            //TODO: check doc
        }
        */



    }
}
