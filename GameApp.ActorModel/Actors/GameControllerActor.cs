using Akka.Actor;
using GameApp.ActorModel.Messages;

namespace GameApp.ActorModel.Actors;

public class GameControllerActor : ReceiveActor
{
    private readonly Dictionary<string, IActorRef> _players;

    public GameControllerActor()
    {
        _players = new Dictionary<string, IActorRef>();

        Receive<JoinGameMessage>(message => JoinGame(message));

        Receive<AttackPlayerMessage>(
            message =>
            {
                _players[message.PlayerName].Forward(message);
                // Forward method maintains the original sender of the message
            });
    }

    private void JoinGame(JoinGameMessage message)
    {
        if (!_players.ContainsKey(message.PlayerName))
        {
            IActorRef newPlayerActor = Context.ActorOf(
                Props.Create(() => new PlayerActor(message.PlayerName)),
                message.PlayerName
            );
            _players.Add(message.PlayerName, newPlayerActor);

            foreach (var player in _players.Values)
            {
                player.Tell(new RefreshPlayerStatusMessage(), Sender); // Sender here is signalR actor
            }
        }
    }
}
