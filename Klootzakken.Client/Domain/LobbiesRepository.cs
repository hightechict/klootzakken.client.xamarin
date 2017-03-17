using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Klootzakken.Client.Data;
using IO.Swagger.Model;

namespace Klootzakken.Client.Domain
{
    public class ApiClientLobbiesRepository : ILobbiesRepository//if bart tels me , hee u dont have to use api, there is dbfor the enrtys...we do not have to change it, rather make a new repository
    {
        private readonly IApiClient _apiClient;

        public ApiClientLobbiesRepository(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        //SOLID!!!! LEARN IT! This is the I, because of Interface segregation (aggregation is the opposite of segregation) -> u depend on IFs ipv. implementations
        public Task<LobbyView> GetLobby() //TODO: maybe delete it coz there is no relevant func
        {
            return _apiClient.GetAsync<LobbyView>("lobby");
        }

        public Task<List<LobbyView>> GetMyLobbies()
        {
            return _apiClient.GetAsync<List<LobbyView>>("myLobbies");
        }

        public Task<List<LobbyView>> GetLobbies()
        {
            return _apiClient.GetAsync<List<LobbyView>>("lobbies");
        }

        public Task<List<LobbyView>> GetMyGames()
        {
            return _apiClient.GetAsync<List<LobbyView>>("myGames");
        }
    }
}