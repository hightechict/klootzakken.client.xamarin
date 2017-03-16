using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Model;
using Klootzakken.Client.App.Interfaces;
using Klootzakken.Client.Data;

namespace Klootzakken.Client.App
{
    public class LobbyStatusService : ILobbyStatusService ////if bart tels me , hee u dont have to use api, there is dbfor the enrtys...we do not have to change it, rather make a new implementation of it
    {
        private IApiClient _apiCLient;

        public LobbyStatusService(IApiClient apiCLient)
        {
            _apiCLient = apiCLient;
        }

        //SOLID!!!! LEARN IT! This is the I, because of Interface segregation (aggregation is the opposite of segregation) -> u depend on IFs ipv. implementations
        public Task<List<LobbyView>> GetLobbiesAsync() // myLobbies, lobbies, myGames
        {
            return _apiCLient.GetAsync<List<LobbyView>>("lobbies");
        }

        public Task<List<LobbyView>> GetMyGamesAsync()
        {
            return _apiCLient.GetAsync<List<LobbyView>>("myGames");
        }

        public Task<List<LobbyView>> GetMyLobbiesAsync()
        {
            return _apiCLient.GetAsync<List<LobbyView>>("myLobbies");
        }

        public Task<LobbyView> GetLobbyStateAsync(string lobbyId)
        {
           return _apiCLient.GetAsync<LobbyView>($"lobby/{lobbyId}");
        }

        public Task<GameView> GetGameStateAsync(string gameId)
        {
            return _apiCLient.GetAsync<GameView>($"game/{gameId}");
        }
    }
}