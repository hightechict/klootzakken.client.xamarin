using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using KlootzakkenClient.cs.Services;
using Klootzakken.Client;

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
            var lobbies = await WebApiGetService.GetLobbies();
            //TODO: catch exception smartly, print error message to the UI
            var isCreateGameSucces = await WebApiPostService.CreateLobbyAsync("gameDani2");
            var joinedToLobby = await WebApiPostService.JoinLobby("1");
            var gameIsStarted = await WebApiPostService.StartGameForLobby("1");

            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGames = new List<string>
            {
                lobbies[0].Name,
                lobbies[3].Name,
                isCreateGameSucces.ToString(),
                joinedToLobby.ToString(),
                gameIsStarted.ToString()
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