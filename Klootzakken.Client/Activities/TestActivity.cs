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

            string randomString  = Guid.NewGuid().ToString("n").Substring(0, 8);

            var isCreateGameSucces = await WebApiPostService.CreateLobbyAsync(randomString);
            var lobbies = await WebApiGetService.GetLobbiesAsync();
            var createdLobby = lobbies.Find(l => l.Name.Equals(randomString));
            var joinedToLobby = await WebApiPostService.JoinLobbyAsync(createdLobby.Id);
            var myLobbyies = await WebApiGetService.GetMyLobbiesAsync();
            var justJoinedMyLobby = myLobbyies.Find(l => l.Name.Equals(randomString));
            var gameIsStarted = await WebApiPostService.StartGameForLobbyAsync(createdLobby.Id);

            //var status = lobbyState.Status.ToString();

            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGames = new List<string>
            {
                "GetLobbiesAsync() - " + randomString.Equals(createdLobby.Name).ToString(),
                "GetMyLobbiesAsync() - " + randomString.Equals(justJoinedMyLobby.Name).ToString(),
                "CreateLobbyAsync() - " + isCreateGameSucces.ToString(),
                "JoinLobbyAsync() - " + joinedToLobby.ToString(),
                "StartGameForLobbyAsync() - " + gameIsStarted.ToString()
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