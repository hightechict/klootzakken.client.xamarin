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

            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://10.0.2.2:5000/") };
            var authenticationService = new AuthenticationService(authenticationOptions);
            var apiClientOptions = new ApiClientOptions() { BaseUri = new Uri("http://10.0.2.2:5000/")};
            var apiClient = new DefaultApiClient(authenticationService, apiClientOptions);
            var lobbyStatusService = new LobbyStatusService(apiClient);
            var lobbyActionService = new LobbyActionService(apiClient);

            var lobbies2 = await lobbyStatusService.GetLobbiesAsync();

            string randomString  = Guid.NewGuid().ToString("n").Substring(0, 8);
            var isCreateGameSucces = await lobbyActionService.CreateLobbyAsync(randomString);
            var lobbies = await lobbyStatusService.GetLobbiesAsync();
            var createdLobby = lobbies.Find(l => l.Name.Equals(randomString));
            var joinedToLobby = await lobbyActionService.JoinLobbyAsync(createdLobby.Id);
            var myLobbyies = await lobbyStatusService.GetMyLobbiesAsync();
            var justJoinedMyLobby = myLobbyies.Find(l => l.Name.Equals(randomString));
            var gameIsStarted = await lobbyActionService.StartGameForLobbyAsync(createdLobby.Id);

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