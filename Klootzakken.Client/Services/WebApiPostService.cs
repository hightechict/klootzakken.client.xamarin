using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace KlootzakkenClient.cs.Services
{
    public class WebApiPostService
    {
        //TODO: finnish the refactor stuff, make a stringContent Builder with buildern pattern
        public static async Task<bool> CreateLobbyAsync(string lobbyName)
        {
            var requestUrl = "http://www.glueware.nl/Klootzakken/kzapi/lobby/create/" + lobbyName;
            var parameter = new StringContent("{\"name\":\"" + lobbyName + "\"}",
                                     Encoding.UTF8,
                                     "application/json");

            return await PostToWebApi(requestUrl, parameter);
        }

        public static async Task<bool> JoinLobby(string lobbyId)
        {
            var requestUrl = "http://www.glueware.nl/Klootzakken/kzapi/lobby/" + lobbyId + "/" + "join";
            var parameter = new StringContent("{\"lobbyId\":\"" + lobbyId + "\"}",
                                    Encoding.UTF8,
                                    "application/json");

            return await PostToWebApi(requestUrl, parameter);
        }

        public static async Task<bool> StartGameForLobby(string lobbyId)
        {
            var requestUrl = "http://www.glueware.nl/Klootzakken/kzapi/lobby/" + lobbyId + "/" + "start";
            var parameter = new StringContent("{\"lobbyId\":\"" + lobbyId + "\"}",
                                    Encoding.UTF8,
                                    "application/json");

            return await PostToWebApi(requestUrl, parameter);
        }

        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0ODgzMDQ4MTAsImV4cCI6MTQ4ODM5MTIxMCwiaWF0IjoxNDg4MzA0ODEwLCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.ctjrQuq_3XItJIWs99jCV4f3uJWX11_7xlzIHtn89KE";

        private static async Task<bool> PostToWebApi(string url, StringContent parameter)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                request.Content = parameter;

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }


    }

}