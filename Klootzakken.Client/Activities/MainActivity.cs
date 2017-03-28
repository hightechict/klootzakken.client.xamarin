using Android.App;
using Android.Widget;
using Android.OS;
using Klootzakken.Client.Activities;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using Klootzakken.Client.Domain;
using System;
using Klootzakken.Client.App.Authentication;
using Klootzakken.Client.Utils;

namespace Klootzakken.Client
{
    [Activity(Label = "KlootzakkenClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase
    {
        private static bool _iocContainerInitialized;

        private const string _authenticationActivityPageKey = "AuthenticationActivity";
        private const string _mainMenuActivityPageKey = "MainMenuActivity";

        protected override void OnCreate(Bundle bundle)
        {
            if (!_iocContainerInitialized)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

                var nav = new NavigationService();
                nav.Configure(_authenticationActivityPageKey, typeof(AuthenticationActivity));
                nav.Configure(_mainMenuActivityPageKey, typeof(MainActivity));
                SimpleIoc.Default.Register<INavigationService>(() => nav);

                var dialog = new DialogService();
                SimpleIoc.Default.Register<IDialogService>(() => dialog);

                var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") };
                var authenticationService = new AuthenticationService(authenticationOptions);
                var tempAuthTokenPoller = new TempAuthTokenPoller(authenticationService);
                var authenticationController = new AuthenticationController(authenticationService, tempAuthTokenPoller, new SharedPreferenceHandler());
                SimpleIoc.Default.Register<AuthenticationController>(() => authenticationController);

                _iocContainerInitialized = true;
            }

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button loginButton = FindViewById<Button>(Resource.Id.btnLogIn);

            loginButton.Click += delegate
            {
                var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                navigationService.NavigateTo(MainActivity._authenticationActivityPageKey);
            };
        }
    }
}