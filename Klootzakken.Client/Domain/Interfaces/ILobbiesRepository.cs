using IO.Swagger.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Klootzakken.Client.Domain
{
    public interface ILobbiesRepository
    {
        //LobbyView is app specific
        //Later on make a converter from Lobby to lobbyview
        Task<LobbyView> GetLobby();

        Task<List<LobbyView>> GetLobbies();

        Task<List<LobbyView>> GetMyLobbies();

        Task<List<LobbyView>> GetMyGames();
    }
}