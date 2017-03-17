using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using KlootzakkenClient.cs.Services;
using Klootzakken.Client;
using System;
using Klootzakken.Client.Resources.Services;
using IO.Swagger.Model;

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

            bool isCreatingLobbySucces = false;
            List<LobbyView> lobbyViews = null;

            try
            {
                isCreatingLobbySucces = await WebApiPostService.CreateLobbyAsync("dani2Lobby");
                lobbyViews = await WebApiGetService.GetLobbiesAsync();

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