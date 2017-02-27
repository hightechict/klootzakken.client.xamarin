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

namespace KlootzakkenClient.cs.Services
{
    public class WebApiPostService
    {
        public static async Task<bool> CreateLobbyAsync(string lobbyName)
        {
            return await PostDataToWebApi(lobbyName);
        }

        private const string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0ODgyMTUzMjksImV4cCI6MTQ4ODMwMTcyOSwiaWF0IjoxNDg4MjE1MzI5LCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.tY2HOrmqrWDY8rdYnCtYzfGtEvJk5EeT6dfdRWUDOPg";

        public static async Task<bool> PostDataToWebApi(string name, bool isPublic = false)
        {
            var success = false;
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://www.glueware.nl/Klootzakken/kzapi/lobby/create/" + name);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                request.Content = new StringContent("{\"name\":\""+ name + "\"}",
                                                    Encoding.UTF8,
                                                    "application/json");

                var response = await client.SendAsync(request);
                success = response.IsSuccessStatusCode;

                return success;
            }
        }
    }

}