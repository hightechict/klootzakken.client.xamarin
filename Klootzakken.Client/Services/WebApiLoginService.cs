using Klootzakken.Client.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Klootzakken.Client.Resources.Services
{
    public static class WebApiLoginService
    {
        private const string _mainWebApiUrl = "http://10.0.2.2:5000/";

        public static string GetPinCode()
        {
            string urlToCreatePin = _mainWebApiUrl + "pin/create/";

            return HttpGetFromWebApi(urlToCreatePin, "pin", NoActionForSettingBearerToken());
        }

        public static string GetTemproraryTokenForAuthentication(string pin)
        {
            string urlForTemporaryTokenForAuth = _mainWebApiUrl + "pin/" + pin + "/token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", NoActionForSettingBearerToken());
        }

        public static string GetGameAccessToken(string temporaryAccessTokenForAuth)
        {
            string urlForTemporaryTokenForAuth = _mainWebApiUrl + "token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", (client) => setBearerAuthenticationParameterForClient(client, temporaryAccessTokenForAuth));
        }

        public static string HttpGetFromWebApi(string url, string resultJsonStringParameterName, SetBearerTokenAction setBearerTokenAction)
        {
            string resultJsonParameterValue = "";

            using (var client = new HttpClient())
            {
                setBearerTokenAction.Invoke(client);
                var response = client.GetAsync(url).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;

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