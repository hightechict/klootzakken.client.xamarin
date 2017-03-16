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
        private readonly ApiClientOptions _options; //use _ for geting rid of "this"

        public DefaultApiClient(IAuthenticationService authenticationService, ApiClientOptions options)
        {
            _authenticationService = authenticationService;
            _options = options;
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
        {    //TODO pass the real token ipv hardcoded one
            return _authenticationService.GetBearerTokenAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwNGIxYmU1NS04MmE5LTRhMTItODMzZC05ZjNlNzZiMTBjMzQiLCJ1bmlxdWVfbmFtZSI6ImRhbmllbC5tb2thQGhpZ2h0ZWNoaWN0Lm5sIiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiI2MWZkZDExNC0wMzRiLTQ3ZDYtYTk1ZS0wZDQ1YzBjZmYwM2YiLCJuYmYiOjE0ODg2NTkxNDEsImV4cCI6MTQ5MTMzNzU0MSwiaWF0IjoxNDg4NjU5MTQxLCJpc3MiOiJLbG9vdHpha2tlbiBTZXJ2ZXIiLCJhdWQiOlsiQXBpVXNlcnMiLCJBcGlVc2VycyJdfQ.V-D6HQSLYVjNOwakMXlsBAbbExpzGhA_kexQSwGHYZE");
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