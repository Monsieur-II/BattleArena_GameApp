using Game.Web.Hubs;
using GameApp.ActorModel.ExternalSystems;
using Microsoft.AspNet.SignalR;
using IHubContext = Microsoft.AspNet.SignalR.IHubContext;

namespace Game.Web.Models;

public class SignalRGameEventsPusher : IGameEventsPusher
{
    private static readonly IHubContext _gameHubContext;

    static  SignalRGameEventsPusher() // static so that the _gameHubContext is initialized only once
    {
        _gameHubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>(); // the signalR hub context
    }
    public void PlayerJoined(string playerName, int playerHealth)
    {
        _gameHubContext.Clients.All.playerJoined(playerName, playerHealth);
        // calls the playerJoined function implemented at the clientside
    }

    public void UpdatePlayerHealth(string playerName, int playerHealth)
    {
        _gameHubContext.Clients.All.updatePlayerHealth(playerName, playerHealth);
        // calls the updatePlayerHealth function implement at clientside
    }
}
