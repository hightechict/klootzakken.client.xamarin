using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Klootzakken.Client.Domain;
using Klootzakken.Client.App.Authentication;

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
            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://10.0.2.2:5000/") };
            var authenticationService = new AuthenticationService(authenticationOptions);

            var authenticationController = new AuthenticationController(authenticationService);

            //Act
            var pinCode = await authenticationController.GetPinCodeAsync();
            var tempAuthToken = await authenticationController.pollingForTemporaryAuthToken(pinCode, 5, 5000);
            var bearerToken = await authenticationController.GetBearerAuthToken(tempAuthToken);

            //assert
            authParameters = new List<string>
            {
                "BearerToken - " + bearerToken
            };

            authParametersView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, authParameters);

            Button createGame = FindViewById<Button>(Resource.Id.btnCreateGame);
            createGame.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}