using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Model;
using Klootzakken.Client.App.Interfaces;
using Klootzakken.Client.Data;

namespace Klootzakken.Client.App
{
    public class LobbyStatusService : ILobbyStatusService
    {
        private IApiClient _apiCLient;

        public LobbyStatusService(IApiClient apiCLient)
        {
            _apiCLient = apiCLient;
        }

        public Task<List<LobbyView>> GetLobbiesAsync() 
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