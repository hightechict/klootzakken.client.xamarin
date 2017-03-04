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

namespace Klootzakken.Client.Activities
{
    [Activity(Label = "TestActivity")]
    public class TestActivity : Activity
    {
        private List<string> myGames;
        private ListView myGamesListView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainMenuView);
            var lobbies = await WebApiGetService.GetLobbies();
            var isCreateGameSucces = await WebApiPostService.CreateLobbyAsync("gameDani2");

            var joinedToLobby = await WebApiPostService.JoinLobby("1");
            var gameIsStarted = await WebApiPostService.StartGameForLobby("1");

            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGames = new List<string>
            {
                lobbies[0].Name,
                lobbies[1].Name,
                isCreateGameSucces.ToString(),
                joinedToLobby.ToString(),
                gameIsStarted.ToString()
            };
            myGamesListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myGames);

            Button createGame = FindViewById<Button>(Resource.Id.btnCreateGame);
            createGame.Click += delegate
            {
                StartActivity(typeof(MainActivity)); 
            };
        }
    }
}