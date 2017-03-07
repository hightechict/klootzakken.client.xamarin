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
using IO.Swagger.Model;
using System.Threading.Tasks;
using Klootzakken.Client.Domain;

namespace Klootzakken.Client.App.Interfaces
{
    public interface ILobbyStatusService
    {
        Task<List<LobbyView>> GetLobbies();

        Task<List<LobbyView>> GetMyLobbies();

        Task<List<LobbyView>> GetMyGames();
    }
}