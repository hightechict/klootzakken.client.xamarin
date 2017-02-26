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
using Android.Webkit;

namespace KlootzakkenClient.cs.Services
{
    public class LoginService
    {

        //get token
        public static async Task PostLoginDataAsync(string tokenValue)
        {
            var token = await GetApiTokenAsync("daniel.moka@hightechict.nl", "Kegedit09#", "http://www.glueware.nl/Klootzakken/kz/Account/", tokenValue);
        }

        public static async Task<string> GetApiTokenAsync(string username, string password, string apiBaseUri, string tokenValue)
        {
            var token = string.Empty;

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                client.Timeout = TimeSpan.FromSeconds(60);

                //set cookie
                var cookie = CookieManager.Instance.GetCookie(apiBaseUri);
                Console.WriteLine(cookie);
                client.DefaultRequestHeaders.Add("Set-Cookie", cookie);
                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
         //new KeyValuePair<string, string>("grant_type", "password"),
         new KeyValuePair<string, string>("Email", username),
         new KeyValuePair<string, string>("Password", password),
         new KeyValuePair<string,string>("__RequestVerificationToken",tokenValue),
         new KeyValuePair<string,string>("RememberMe","false")

                  });

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Login?ReturnUrl=%2FKlootzakken%2Fkz%2Ftoken");
                request.Content = formContent;

                //var response = await client.SendAsync(request);

                await client.SendAsync(request)
                      .ContinueWith(responseTask => Console.WriteLine("Response: {0}", responseTask.Result));

                //send request               
                //HttpResponseMessage responseMessage = await client.PostAsync("/Login?returnurl=%2FKlootzakken%2Fkz%2FToken/", formContent);
                //var responseJson = await responseMessage.Content.ReadAsStringAsync();
                /*var jObject = JObject.Parse(responseJson);
                token = jObject.GetValue("access_token").ToString();*/

                return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0ODgwNDI2ODUsImV4cCI6MTQ4ODEyOTA4NSwiaWF0IjoxNDg4MDQyNjg1LCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.etShuzx9dHfnItthHtgOXA64S0sh7C2g3irqfJU1LpE";
            }
        }
}