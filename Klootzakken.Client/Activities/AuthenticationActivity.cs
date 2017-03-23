using Android.App;
using Android.Widget;
using Android.OS;
using Klootzakken.Client.Activities;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using System;
using Klootzakken.Client.App.Authentication;
using Android.Content;
using Klootzakken.Client.Utils;

namespace Klootzakken.Client
{
    [Activity(Label = "KlootzakkenClient", MainLauncher = false, Icon = "@drawable/icon")]
    public class AuthorizationActivity : ActivityBase
    {
        public const string maniMenuPageKey = "MainMenuActivity";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AuthenticationView);

            var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            var nav = ServiceLocator.Current.GetInstance<INavigationService>();

            Button generatePinButton = FindViewById<Button>(Resource.Id.generatePin);

            generatePinButton.Click += async delegate
            {
                var authenticationController = ServiceLocator.Current.GetInstance<AuthenticationController>();
                var pinCode = await authenticationController.GetPinCodeAsync();

                await dialogService.ShowMessage(
                   pinCode,
                    "Pair the following pincode",
                    "Log in",
                    "Cancel",
                    new Action<bool>((isConfirmed) => authenticationController.SaveBearerAuthTokenAsync(pinCode)));
                //TODO: add false
                //TODO: add test for services seperately with the live server
            };

            RunOnUiThread(() => Toast.MakeText(this, new SharedPreferenceHandler().GetPreference("bearer_token"), ToastLength.Long).Show());
        }
    }
}