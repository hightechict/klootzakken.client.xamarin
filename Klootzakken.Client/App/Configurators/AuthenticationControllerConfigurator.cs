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
using Klootzakken.Client.App.Authentication;
using GalaSoft.MvvmLight.Ioc;
using Klootzakken.Client.Utils;
using Klootzakken.Client.Utils.Interfaces;

namespace Klootzakken.Client.App.Configurators
{
    public class AuthenticationControllerConfigurator
    {
        private Uri _klootzakkenWebUri = new Uri("http://www.glueware.nl/klootzakken/kz/");

        private ISharedPreferenceHandler _sharedPreferenceHander = new SharedPreferenceHandler();

        public void Configure() {
            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") };
            var authenticationService = new AuthenticationService(new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") });
            var tempAuthTokenPoller = new TempAuthTokenPoller(authenticationService);

            var authenticationController = new AuthenticationController(authenticationService, tempAuthTokenPoller, _sharedPreferenceHander);

            SimpleIoc.Default.Register<AuthenticationController>(() => authenticationController);
        }
    }
}