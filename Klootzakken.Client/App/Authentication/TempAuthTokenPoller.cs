using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Klootzakken.Client.Domain;
using System.Threading;
using System.Threading.Tasks;
using Klootzakken.Client.App.Interfaces;

namespace Klootzakken.Client.App.Authentication
{
    public class TempAuthTokenPoller
    {
        private IAuthenticationService _authenticationService;

        public TempAuthTokenPoller(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> poll(string pinCode, int totalNumberOfAttempts, int delayBetweenEachPollingInMilisec) //TODO: get vars from options or config
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