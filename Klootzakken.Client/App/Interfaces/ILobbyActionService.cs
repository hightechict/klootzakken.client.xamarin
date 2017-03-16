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
using System.Threading.Tasks;

namespace Klootzakken.Client.App.Interfaces
{
    public interface ILobbyActionService
    {

        Task<bool> CreateLobbyAsync(string lobbyName);

        Task<bool> JoinLobbyAsync(string lobbyId);

        Task<bool> StartGameForLobbyAsync(string lobbyId);

    }
}