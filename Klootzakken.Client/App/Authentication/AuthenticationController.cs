using System;
using Klootzakken.Client.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace Klootzakken.Client.App.Authentication
{
    public class AuthenticationController
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> GetPinCodeAsync()
        {
            return await _authenticationService.GetPinAsync();
        }

        public async Task<string> pollingForTemporaryAuthToken(string pinCode, int totalNumberOfAttempts, int delayBetweenEachPollingInMillisec) //TODO: get vars from options or config
        {
            var numberOfAttempts = 0;
            while (true)
            {
                try
                {
                    return await _authenticationService.GetTemporaryAuthTokenAsync(pinCode);
                }
                catch(PinAuthenticationException ex)
                {
                    numberOfAttempts++;
                    if (numberOfAttempts >= totalNumberOfAttempts)
                        throw; //TODO: do we want to return?
                    Thread.Sleep(delayBetweenEachPollingInMillisec);
                    Console.WriteLine(ex); //TODO: write error to an error blok
                }
            }
        }
    }
}