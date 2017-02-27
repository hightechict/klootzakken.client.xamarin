using System.Collections.Generic;
using System.Threading.Tasks;
using IO.Swagger.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace KlootzakkenClient
{
    public static class WebApiGetService
    {
        public static async Task<List<LobbyView>> GetMyGames()
        {
            return await GetFromWebApi<LobbyView>("myGames");
        }

        public static async Task<List<LobbyView>> GetMyLobbies()
        {
            return await GetFromWebApi<LobbyView>("myLobbies");
        }

        public static async Task<List<LobbyView>> GetLobbies()
        {
            return await GetFromWebApi<LobbyView>("lobbies");
        }

        public static async Task<GameView> GetGameState(string gameId)
        {
            return await GetStateFromWebApi<GameView>("game", gameId);
        }

        public static async Task<LobbyView> GetLobbyState(string lobbyId)
        {
            return await GetStateFromWebApi<LobbyView>("lobby", lobbyId);
        }


        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0ODgyMTUzMjksImV4cCI6MTQ4ODMwMTcyOSwiaWF0IjoxNDg4MjE1MzI5LCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.tY2HOrmqrWDY8rdYnCtYzfGtEvJk5EeT6dfdRWUDOPg";

        private static async Task<List<T>> GetFromWebApi<T>(string urlEnding)
        {
            var views = new List<T>();

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://www.glueware.nl/Klootzakken/kzapi/" + urlEnding);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                views = JsonConvert.DeserializeObject<List<T>>(content);
            }

            return views;
        }

        private static async Task<T> GetStateFromWebApi<T>(string urlEnding, string id)
        {
            T state;

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://www.glueware.nl/Klootzakken/kzapi/" + urlEnding + "/" + id);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                state = JsonConvert.DeserializeObject<T>(content);
            }

            return state;
        }

    }
}