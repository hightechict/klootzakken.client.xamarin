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
using KlootzakkenClient.cs.Services;
using KlootzakkenClient;
using IO.Swagger.Model;
using System.Threading.Tasks;
using Klootzakken.Client.Resources.Services;
using Klootzakken.Client.App.Interfaces;
using Klootzakken.Client.Data;
using Klootzakken.Client.Domain;
using Klootzakken.Client.App;
using Klootzakken.Client.App.GameApiService;
using Klootzakken.Client.App.Authentication;
using System.Threading;

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

            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://10.0.2.2:5000/") };
            var authenticationService = new AuthenticationService(authenticationOptions);

            var authenticationController = new AuthenticationController(authenticationService);

            var pinCode = await authenticationController.GetPinCodeAsync();
            var tempAuthToken = await authenticationController.pollingForTemporaryAuthToken(pinCode, 5, 5000);
            var bearerToken = await authenticationController.GetBearerAuthToken(tempAuthToken);

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