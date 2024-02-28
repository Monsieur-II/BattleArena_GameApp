using Akka.Actor;
using GameApp.ActorModel.ExternalSystems;
using GameApp.ActorModel.Messages;

namespace GameApp.ActorModel.Actors;

public class SignalRBridgeActor : ReceiveActor
{
    private readonly IGameEventsPusher _gameEventsPUsher; // to send events to the outside world
    
    private readonly IActorRef _gameController;
    // to send events to the game controller

    public SignalRBridgeActor(IGameEventsPusher gameEventsPUsher, IActorRef gameController)
    {
        _gameEventsPUsher = gameEventsPUsher;
        _gameController = gameController;

        Receive<JoinGameMessage>(
            message =>
            {
                _gameController.Tell(message);
            });

        Receive<AttackPlayerMessage>(
            message =>
            {
                _gameController.Tell(message);
            });
        
        Receive<PlayerStatusMessage>(
            message =>
            {
                _gameEventsPUsher.PlayerJoined(message.PlayerName, message.Health);
            });
        
        Receive<PlayerHealthChangedMessage>(
            message =>
            {
                _gameEventsPUsher.UpdatePlayerHealth(message.PlayerName, message.Health);
            });
    }
}   
