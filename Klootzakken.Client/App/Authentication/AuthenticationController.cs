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

        public AuthenticationController(IAuthenticationService authenticationService, ISharedPreferenceHandler preferenceHandler)
        {
            _authenticationService = authenticationService;
            _preferenceHandler = preferenceHandler;
        }

        public async Task<string> GetPinCodeAsync()
        {
            return await _authenticationService.GetPinAsync();
        }

        public async void SaveBearerAuthTokenAsync(string pinCode)
        {
            if (_preferenceHandler.GetPreference(_brearerTokenKeyName) is null)
            {
                string bearerToken = await GetBrearerTokenAsync(pinCode);
                _preferenceHandler.SavePreference("bearer_token", bearerToken);
            }
        }

        private async Task<string> GetBrearerTokenAsync(string pinCode)
        {
            var tempToken = await pollingForTemporaryAuthToken(pinCode, 5, 5000);
            var bearerToken = await _authenticationService.GetBearerTokenAsync(tempToken);
            return bearerToken;
        }

        public async Task<string> pollingForTemporaryAuthToken(string pinCode, int totalNumberOfAttempts, int delayBetweenEachPollingInMilisec) //TODO: get vars from options or config
        {
            var numberOfAttempts = 0;
            while (true)
            {
                try
                {
                    return await _authenticationService.GetTemporaryAuthTokenAsync(pinCode);
                }
                catch (PinAuthenticationException ex)
                {
                    numberOfAttempts++;
                    if (numberOfAttempts >= totalNumberOfAttempts)
                        throw; //TODO: do we want to return?
                    Thread.Sleep(delayBetweenEachPollingInMilisec);
                }
            }
        }

    }
}