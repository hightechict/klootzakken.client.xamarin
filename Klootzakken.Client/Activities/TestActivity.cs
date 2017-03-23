using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
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

            System.Threading.ThreadPool.SetMinThreads(30, 30);

            //Arrange
            var authenticationOptions = new AuthenticationOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kz/") };
            var authenticationService = new AuthenticationService(authenticationOptions);
            var apiClientOptions = new ApiClientOptions() { BaseUri = new Uri("http://www.glueware.nl/klootzakken/kzapi/") };
            var apiClient = new DefaultApiClient(authenticationService, apiClientOptions);
            var lobbyStatusService = new LobbyStatusService(apiClient);
            var lobbyActionService = new LobbyActionService(apiClient);

            //Act
            string randomString  = Guid.NewGuid().ToString("n").Substring(0, 8);

            var isCreateGameSucces = await lobbyActionService.CreateLobbyAsync(randomString);

            var lobbies = await lobbyStatusService.GetLobbiesAsync();
            var createdLobby = lobbies.Find(l => l.Name.Equals(randomString));
            var joinedToLobby = await lobbyActionService.JoinLobbyAsync(createdLobby.Id);
            var myLobbyies = await lobbyStatusService.GetMyLobbiesAsync();
            var justJoinedMyLobby = myLobbyies.Find(l => l.Name.Equals(randomString));
            var gameIsStarted = await lobbyActionService.StartGameForLobbyAsync(createdLobby.Id);

            //Assert
            myGames = new List<string>
            {
                "GetLobbiesAsync() - " + randomString.Equals(createdLobby.Name).ToString(),
                "GetMyLobbiesAsync() - " + randomString.Equals(justJoinedMyLobby.Name).ToString(),
                "CreateLobbyAsync() - " + isCreateGameSucces.ToString(),
                "JoinLobbyAsync() - " + joinedToLobby.ToString(),
                "StartGameForLobbyAsync() - " + gameIsStarted.ToString()
            };
            myGamesListView = FindViewById<ListView>(Resource.Id.myGamesListView);
            myGamesListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myGames);

            Button createGame = FindViewById<Button>(Resource.Id.btnCreateGame);
            createGame.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}