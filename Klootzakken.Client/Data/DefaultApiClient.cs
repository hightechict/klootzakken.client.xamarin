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
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Klootzakken.Client.Domain;

namespace Klootzakken.Client.Data
{
    public class DefaultApiClient : IApiClient
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ApiClientOptions _options; //use _ for gettting rid of "this"

        public DefaultApiClient(IAuthenticationService authenticationService, ApiClientOptions options)
        {
            _options = options;
            _authenticationService = authenticationService;
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string path)
        {
            using (var client = new HttpClient())
            {
                var bearerToken = await GetTokenIfNotExistingAsync();
                var uri = new Uri(_options.BaseUri, path); //WHY URI: this will make sure that uri correct
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", bearerToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }

        }

        private Task<string> GetTokenIfNotExistingAsync()
        {
            return _authenticationService.GetBearerTokenAsync("temporaryId"); //todo pass the real temp token
        }

        public Task PostAsync()
        {
            throw new NotImplementedException();
        }

        public Task PutAsync()
        {
            throw new NotImplementedException();
        }
    }
}