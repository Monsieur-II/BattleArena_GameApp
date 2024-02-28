using Microsoft.AspNet.SignalR;
using GameApp.ActorModel.Actors;
using GameApp.ActorModel.Messages;
using Akka.Actor;
using Game.Web.Models;

namespace Game.Web.Hubs;

public class GameHub : Hub
{
    public void JoinGame(string playerName)
    {
        GameActorSystem
            .ActorReferences
            .SignalRBridge
            .Tell(new JoinGameMessage(playerName));
    }

    public void Attack(string playerName)
    {
        GameActorSystem
            .ActorReferences
            .SignalRBridge
            .Tell(new AttackPlayerMessage(playerName));
    }
}
