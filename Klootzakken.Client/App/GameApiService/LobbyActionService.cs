using System.Text;
using System.Threading.Tasks;
using Klootzakken.Client.App.Interfaces;
using Klootzakken.Client.Data;
using Klootzakken.Client.Utils;

namespace Klootzakken.Client.App.GameApiService
{
    public class LobbyActionService : ILobbyActionService
    {
        private IApiClient _apiClient;

        private StringContentBuilder stringContentBuilder = new StringContentBuilder(Encoding.UTF8, "application/json");

        public LobbyActionService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> CreateLobbyAsync(string lobbyName)
        {
            return await _apiClient.PostAsync(
                $"lobby/create/{lobbyName}",
                stringContentBuilder.Build(KeyValuePairCreator.Create("name", lobbyName)));
        }

        public async Task<bool> JoinLobbyAsync(string lobbyId)
        {
            return await _apiClient.PostAsync(
                $"lobby/{lobbyId}/join",
                stringContentBuilder.Build(KeyValuePairCreator.Create("lobbyId", lobbyId)));
        }

        public async Task<bool> StartGameForLobbyAsync(string lobbyId)
        {
            return await _apiClient.PostAsync(
                $"lobby/{lobbyId}/start",
                stringContentBuilder.Build(KeyValuePairCreator.Create("lobbyId", lobbyId)));
        }
    }
}