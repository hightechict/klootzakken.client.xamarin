using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Klootzakken.Client.Domain;
using Klootzakken.Client.App.Authentication;
using Klootzakken.Client.Utils;

namespace Klootzakken.Client.Activities
{
    [Activity(Label = "TestActivityWebApi")]
    public class TestActivityWebApi : Activity
    {
        private List<string> authParameters;
        private ListView authParametersView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainMenuView);

            //Arrange
            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") };
            var authenticationService = new AuthenticationService(authenticationOptions);
            var tempAuthTokenPoller = new TempAuthTokenPoller(authenticationService);

            var authenticationController = new AuthenticationController(authenticationService, tempAuthTokenPoller, new SharedPreferenceHandler());

            //Act
            var pinCode = await authenticationController.GetPinCodeAsync();
            //var tempAuthToken = await authenticationController.pollingForTemporaryAuthToken(pinCode, 5, 5000);
            //var bearerToken = await authenticationController.SaveBearerAuthTokenAsync(tempAuthToken);

            //Assert
            /*authParameters = new List<string>
            {
                "BearerToken - " + bearerToken
            };*/

            authParametersView = FindViewById<ListView>(Resource.Id.myGamesListView);
            authParametersView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, authParameters);

            Button createGame = FindViewById<Button>(Resource.Id.btnCreateGame);
            createGame.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}