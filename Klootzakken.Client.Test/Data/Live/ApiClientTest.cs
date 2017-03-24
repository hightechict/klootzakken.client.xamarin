/*
 This tests are touching the live server so are failing if there is no connection to the glueware.nl server.
 The purpose of these tests is to test the basic functionality of getAsync end postAsync to the RESTApi
 */

using FluentAssertions;
using IO.Swagger.Model;
using Klootzakken.Client.Data;
using Klootzakken.Client.Domain;
using Klootzakken.Client.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Klootzakken.Client.Test.Data.Live
{
    public class ApiClientTest
    {
        private StringContentBuilder stringContentBuilder = new StringContentBuilder(Encoding.UTF8, "application/json");

        [Fact]
        public async void setUpClient_getAsync_asyncMethodIsSucces()
        {
            //Arrange
            var sut = SetUpApiClient();

            //Act
            var lobbies = await sut.GetAsync<List<LobbyView>>("lobbies");

            //Assert
            lobbies.Should().NotBeNull();
        }

        [Fact]
        public async void setUpClient_postAsync_asyncMethodIsSucces()
        {
            //Arrange
            var lobbyName = "danielsLobby";
            var path = $"lobby/create/{lobbyName}";
            var postParams = stringContentBuilder.Build(KeyValuePairCreator.Create("name", lobbyName));
            var sut = SetUpApiClient();

            //Act
            var isPosted = await sut.PostAsync(path , postParams);

            //Assert
            isPosted.Should().BeTrue();
        }

        private IApiClient SetUpApiClient()
        {
            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") };
            var authenticationService = new AuthenticationService(authenticationOptions);
            var apiClientOptions = new ApiClientOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kzapi/") };
            return new DefaultApiClient(authenticationService, apiClientOptions);
        }
    }
}
