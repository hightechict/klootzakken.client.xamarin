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
        public static async Task<bool> CreateLobbyAsync(string lobbyName)
        {
            return await PostToWebApi(lobbyName);
        }

        public static async Task<bool> JoinLobby(string lobbyId)
        {
            return await PostActionWithLobby("join", lobbyId);
        }

        public static async Task<bool> StartGameForLobby(string lobbyId)
        {
            return await PostActionWithLobby("start", lobbyId);
        }

        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0ODgyMTUzMjksImV4cCI6MTQ4ODMwMTcyOSwiaWF0IjoxNDg4MjE1MzI5LCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.tY2HOrmqrWDY8rdYnCtYzfGtEvJk5EeT6dfdRWUDOPg";

        private static async Task<bool> PostToWebApi(string name, bool isPublic = false)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://www.glueware.nl/Klootzakken/kzapi/lobby/create/" + name);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                request.Content = new StringContent("{\"name\":\"" + name + "\"}",
                                                    Encoding.UTF8,
                                                    "application/json");

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        private static async Task<bool> PostActionWithLobby(string action, string lobbyId)
        {
            using (var client = new HttpClient())
            {   //TODO: refactor it because has duplication with PostToWebApi
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://www.glueware.nl/Klootzakken/kzapi/lobby/" + lobbyId + "/" + action);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                request.Content = new StringContent("{\"lobbyId\":\"" + lobbyId + "\"}",
                                                    Encoding.UTF8,
                                                    "application/json");

                var response = await client.SendAsync(request);
                //TODO: handle error message (eg when starting game and getting 400 error because a game must have between 2-6 players)
                return response.IsSuccessStatusCode;
            }
        }
    }

}