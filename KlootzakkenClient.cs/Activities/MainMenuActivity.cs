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
using KlootzakkenClient;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using IO.Swagger.Model;
using KlootzakkenClient.cs;

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

            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGames = new List<string>
            {
                lobbies[0].Name,
                lobbies[1].Name
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