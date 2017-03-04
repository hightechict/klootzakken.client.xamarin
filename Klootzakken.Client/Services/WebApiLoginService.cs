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

            return HttpGetFromWebApi(urlToCreatePin, "pin", NoAction());
        }

        public static string GetTemproraryTokenForAuthentication(string pin)
        {
            string urlForTemporaryTokenForAuth = _mainWebApiUrl + "pin/" + pin + "/token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", NoAction());
        }

        public static string GetGameAccessToken(string temporaryAccessTokenForAuth)
        {
            string urlForTemporaryTokenForAuth = _mainWebApiUrl + "token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", (client) => setBearerAuthenticationParameterForClient(client, temporaryAccessTokenForAuth));
        }

        public static string HttpGetFromWebApi(string url, string resultJsonStringParameterName, Action<HttpClient> actionToSetBearerToken)
        {
            string result = "";

            using (var client = new HttpClient())
            {
                actionToSetBearerToken.Invoke(client);
                var response = client.GetAsync(url).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;

                result = JsonStringParser.GetValue(resultJsonStringParameterName, resultJson);
                //TODO: handle error
            }
            return result;
        }

        private static Action<HttpClient> NoAction()
        {
            return (client) => { };
        }

        public static void setBearerAuthenticationParameterForClient(HttpClient client, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }
}