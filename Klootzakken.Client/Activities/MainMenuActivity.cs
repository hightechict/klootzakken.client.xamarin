using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Klootzakken.Client;
using System;
using IO.Swagger.Model;
using Klootzakken.Client.Data;
using Klootzakken.Client.Domain;
using Klootzakken.Client.App;
using Klootzakken.Client.App.GameApiService;

namespace KlootzakkenClient.Activities
{
    [Activity(Label = "MainMenuActivity")]
    public class MainMenuActivity : Activity
    {

        private List<string> myGames;
        private ListView myGamesListView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainMenuView);

            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://10.0.2.2:5000/") };
            var authenticationService = new AuthenticationService(authenticationOptions);
            var apiClientOptions = new ApiClientOptions() { BaseUri = new Uri("http://10.0.2.2:5000/") };
            var apiClient = new DefaultApiClient(authenticationService, apiClientOptions);
            var lobbyStatusService = new LobbyStatusService(apiClient);
            var lobbyActionService = new LobbyActionService(apiClient);

            bool isCreatingLobbySucces = false;
            List<LobbyView> lobbyViews = null;

            try
            {
                isCreatingLobbySucces = await lobbyActionService.CreateLobbyAsync("dani2Lobby");
                lobbyViews = await lobbyStatusService.GetLobbiesAsync();

            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGames = new List<string>
            {
                isCreatingLobbySucces.ToString()
            };
            myGamesListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myGames);

            Button createGame = FindViewById<Button>(Resource.Id.btnCreateGame);
            createGame.Click += delegate
            {
                StartActivity(typeof(MainActivity)); //TODO: make a new activity for creating new game
            };
        }
    }
}