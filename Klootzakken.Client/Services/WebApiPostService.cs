using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Klootzakken.Client.Utils;
using System.Text;
using System.Collections.Generic;

namespace KlootzakkenClient.cs.Services
{
    public static class WebApiPostService
    {
        private const string _baseUrl = "http://10.0.2.2:5000/";

        private static StringContent CreateStringContent(KeyValuePair<string, string> keyValuePair) => new StringContentBuilder(Encoding.UTF8, "application/json").build(keyValuePair);

        public static async Task<bool> CreateLobbyAsync(string lobbyName)
        {
            var requestUrl = _baseUrl + "lobby/create/" + lobbyName;

            return await PostToWebApi(requestUrl, CreateStringContent(KeyValuePairCreator.Create<string, string>("name", lobbyName)));
        }

        public static async Task<bool> JoinLobbyAsync(string lobbyId)
        {
            var requestUrl = _baseUrl + "lobby/" + lobbyId + "/" + "join";

            return await PostToWebApi(requestUrl, CreateStringContent(KeyValuePairCreator.Create<string, string>("lobbyId", lobbyId)));
        }

        public static async Task<bool> StartGameForLobbyAsync(string lobbyId)
        {
            var requestUrl = _baseUrl + "lobby/" + lobbyId + "/" + "start";

            return await PostToWebApi(requestUrl, CreateStringContent(KeyValuePairCreator.Create<string, string>("lobbyId", lobbyId)));
        }

        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwNGIxYmU1NS04MmE5LTRhMTItODMzZC05ZjNlNzZiMTBjMzQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI2MWZkZDExNC0wMzRiLTQ3ZDYtYTk1ZS0wZDQ1YzBjZmYwM2YiLCJuYmYiOjE0ODg2NTkxNDEsImV4cCI6MTQ5MTMzNzU0MSwiaWF0IjoxNDg4NjU5MTQxLCJpc3MiOiJLbG9vdHpha2tlbiBTZXJ2ZXIiLCJhdWQiOlsiQXBpVXNlcnMiLCJBcGlVc2VycyJdfQ.V-D6HQSLYVjNOwakMXlsBAbbExpzGhA_kexQSwGHYZE";

        private static async Task<bool> PostToWebApi(string url, StringContent requestContent)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                request.Content = requestContent;

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

    }
}