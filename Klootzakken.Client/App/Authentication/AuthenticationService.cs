using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Klootzakken.Client.Utils;
using System.Net.Http.Headers;

namespace Klootzakken.Client.Domain
{
    public class AuthenticationService : IAuthenticationService
    {
        private AuthenticationOptions _options;

        public AuthenticationService(AuthenticationOptions options)
        {
            _options = options;
        }

        public Task<string> GetBearerTokenAsync(string temporaryToken)
        {
            string urlForTemporaryTokenForAuth = _options.BaseUri + "token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", (client) => setBearerAuthenticationParameterForClient(client, "temp"));
        }

        public Task<string> GetPinAsync()
        {
            string urlToCreatePin = _options.BaseUri + "pin/create/";

            return HttpGetFromWebApi(urlToCreatePin, "pin", NoActionForSettingBearerToken());
        }

        public Task<string> GetTemporaryAuthToken(string pin)
        {
            string urlForTemporaryTokenForAuth = _options.BaseUri + "pin/" + pin + "/token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", NoActionForSettingBearerToken());
        }

        public static async Task<string> HttpGetFromWebApi(string url, string resultJsonStringParameterName, SetBearerTokenAction setBearerTokenAction)
        {
            string resultJsonParameterValue = "";

            using (var client = new HttpClient())
            {
                setBearerTokenAction.Invoke(client);
                var response = await client.GetAsync(url);
                var resultJson = await response.Content.ReadAsStringAsync();

                resultJsonParameterValue = JsonStringParser.GetValue(resultJsonStringParameterName, resultJson);
                //TODO: handle error
            }
            return resultJsonParameterValue;
        }

        public delegate void SetBearerTokenAction(HttpClient client);

        private static SetBearerTokenAction NoActionForSettingBearerToken()
        {
            return (client) =>
            {
            };
        }

        private static void setBearerAuthenticationParameterForClient(HttpClient client, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }
}