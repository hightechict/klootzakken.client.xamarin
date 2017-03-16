using System.Threading.Tasks;
using System.Net.Http;
using Klootzakken.Client.Utils;
using System.Net.Http.Headers;
using Klootzakken.Client.App.Authentication;

namespace Klootzakken.Client.Domain
{
    public class AuthenticationService : IAuthenticationService
    {
        private AuthenticationOptions _options;

        public AuthenticationService(AuthenticationOptions options)
        {
            _options = options;
        }

        public Task<string> GetPinAsync()
        {
            string urlToCreatePin = _options.BaseUri + "pin/create/";

            return HttpGetFromWebApi(urlToCreatePin, "pin", NoActionForSettingBearerToken());
        }

        public Task<string> GetTemporaryAuthTokenAsync(string pin)
        {
            string urlForTemporaryTokenForAuth = _options.BaseUri + "pin/" + pin + "/token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", NoActionForSettingBearerToken());
        }

        public Task<string> GetBearerTokenAsync(string temporaryToken)
        {
            string urlForTemporaryTokenForAuth = _options.BaseUri + "token";

            return HttpGetFromWebApi(urlForTemporaryTokenForAuth, "access_token", (client) => setBearerAuthenticationParameterForClient(client, temporaryToken));
        }

        private static async Task<string> HttpGetFromWebApi(string url, string resultJsonStringParameterName, SetBearerTokenAction setBearerTokenAction)
        {
            string resultJsonParameterValue = "";

            using (var client = new HttpClient())
            {
                setBearerTokenAction.Invoke(client);
                var response = await client.GetAsync(url);

                var resultJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resultJsonParameterValue = JsonStringParser.GetValue(resultJsonStringParameterName, resultJson);
                }
                else
                {
                    throw new PinAuthenticationException(response.StatusCode.ToString()); //TODO: map from status code to the error
                   //TODO: make and use service exception                                                                 
                }
            }
            return resultJsonParameterValue;
        }

        private delegate void SetBearerTokenAction(HttpClient client);

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