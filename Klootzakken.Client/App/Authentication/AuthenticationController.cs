using System;
using Klootzakken.Client.Domain;
using System.Threading.Tasks;
using System.Threading;
using Android.App;
using Android.Content;
using Klootzakken.Client.Utils.Interfaces;

namespace Klootzakken.Client.App.Authentication
{
    public class AuthenticationController
    {
        private const string _brearerTokenKeyName = "bearer_token";

        private IAuthenticationService _authenticationService;
        private ISharedPreferenceHandler _preferenceHandler;

        private TempAuthenticationTokenPoller _tempAuthenticationTokenPoller;

        public AuthenticationController(IAuthenticationService authenticationService, ISharedPreferenceHandler preferenceHandler)
        {
            _authenticationService = authenticationService;
            _preferenceHandler = preferenceHandler;

            _tempAuthenticationTokenPoller = new TempAuthenticationTokenPoller(authenticationService);
        }

        public async Task<string> GetPinCodeAsync()
        {
            return await _authenticationService.GetPinAsync();
        }

        public async void SaveBearerAuthTokenAsync(string pinCode)
        {
            if (_preferenceHandler.GetPreference(_brearerTokenKeyName) is null) //TODO: also handler the situ when the key is expired
            {
                string bearerToken = await GetBrearerTokenAsync(pinCode);
                _preferenceHandler.SavePreference("bearer_token", bearerToken);
            }

        }

        private async Task<string> GetBrearerTokenAsync(string pinCode)
        {
            var tempToken = await _tempAuthenticationTokenPoller.poll(pinCode, 5, 5000); 
            var bearerToken = await _authenticationService.GetBearerTokenAsync(tempToken);
            return bearerToken;
        }
    }
}