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
        private const string _bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0OTAwMjkyNzIsImV4cCI6MTQ5MjcwNzY3MiwiaWF0IjoxNDkwMDI5MjcyLCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6WyJEZW1vQXVkaWVuY2UiLCJEZW1vQXVkaWVuY2UiXX0._Hxv-wzFP_cI2bHUBmile_aBRZScv9uXcinzTH8l5Eg";

        private readonly IAuthenticationService _authenticationService;
        private readonly ApiClientOptions _options; 

        public DefaultApiClient(IAuthenticationService authenticationService, ApiClientOptions options)
        {
            _authenticationService = authenticationService;
            _options = options;
        }

        public async Task<T> GetAsync<T>(string path)
        {
            using (var client = new HttpClient())
            {
                //var bearerToken = await GetTokenIfNotExistingAsync();

                var uri = new Uri(_options.BaseUri, path); //WHY URI: this will make sure that uri correct
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _bearerToken);

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }
        }




        private Task<string> GetTokenIfNotExistingAsync()
        {    //TODO pass the real token ipv hardcoded one
            return _authenticationService.GetBearerTokenAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3YmUyMjI1NS0xMTFkLTQyNjUtYjkzNi0zY2I3NDQ2NWVmZGQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI4YTIyMTcwMC1kYjYzLTRiOWYtYWNiOC1mZTJjMDFiOWZjZmMiLCJuYmYiOjE0OTAwMjg2NTksImV4cCI6MTQ5MjcwNzA1OSwiaWF0IjoxNDkwMDI4NjU5LCJpc3MiOiJEaXZ2ZXJlbmNlLmNvbSBLbG9vdHpha2tlbiIsImF1ZCI6IkRlbW9BdWRpZW5jZSJ9.wBLQSdLsf49hXN_ocxQsvLssA2koyMlQyUntbysiXCo");
        }

        public async Task<bool> PostAsync(string path, StringContent postParameters)
        {
            using (var client = new HttpClient())
            {
                //var bearerToken = await GetTokenIfNotExistingAsync();
                var uri = new Uri(_options.BaseUri, path); //WHY URI: this will make sure that uri correct
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _bearerToken);
                request.Content = postParameters;

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        public Task PutAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}