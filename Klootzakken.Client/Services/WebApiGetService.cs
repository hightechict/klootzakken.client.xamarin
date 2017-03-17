using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace KlootzakkenClient
{
    public static class WebApiGetService //TODO: REMOVE STATICS
    {
        //TODO: refactor it like in PostService
        public static async Task<List<LobbyView>> GetMyGamesAsync()
        {
            return await GetFromWebApiAsync<LobbyView>("myGames");
        }

        public static async Task<List<LobbyView>> GetMyLobbiesAsync()
        {
            return await GetFromWebApiAsync<LobbyView>("myLobbies");
        }

        public static async Task<List<LobbyView>> GetLobbiesAsync()
        {
            return await GetFromWebApiAsync<LobbyView>("lobbies");
        }

        public static async Task<GameView> GetGameStateAsync(string gameId)
        {
            return await GetStateFromWebApiAsync<GameView>("game", gameId);
        }

        public static async Task<LobbyView> GetLobbyStateAsync(string lobbyId)
        {
            return await GetStateFromWebApiAsync<LobbyView>("lobby", lobbyId);
        }

        private const string _baseUrl = "http://10.0.2.2:5000/";
        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwNGIxYmU1NS04MmE5LTRhMTItODMzZC05ZjNlNzZiMTBjMzQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI2MWZkZDExNC0wMzRiLTQ3ZDYtYTk1ZS0wZDQ1YzBjZmYwM2YiLCJuYmYiOjE0ODg2NTkxNDEsImV4cCI6MTQ5MTMzNzU0MSwiaWF0IjoxNDg4NjU5MTQxLCJpc3MiOiJLbG9vdHpha2tlbiBTZXJ2ZXIiLCJhdWQiOlsiQXBpVXNlcnMiLCJBcGlVc2VycyJdfQ.V-D6HQSLYVjNOwakMXlsBAbbExpzGhA_kexQSwGHYZE";

        private static async Task<List<T>> GetFromWebApiAsync<T>(string urlEnding)
        {
            var views = new List<T>();

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + urlEnding);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                views = JsonConvert.DeserializeObject<List<T>>(content);
            }

            return views;
        }

        private static async Task<T> GetStateFromWebApiAsync<T>(string urlEnding, string id)
        {
            T state;

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + urlEnding + "/" + id);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                state = JsonConvert.DeserializeObject<T>(content);
            }

            return state;
        }
}
}