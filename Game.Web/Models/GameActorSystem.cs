using Akka.Actor;
using Akka.Actor.Dsl;
using GameApp.ActorModel.Actors;
using GameApp.ActorModel.ExternalSystems;

namespace Game.Web.Models;

public static class GameActorSystem
{
    private static ActorSystem _actorSystem;
    private static IGameEventsPusher _gameEventsPusher;

    public static void Create()
    {
        _gameEventsPusher = new SignalRGameEventsPusher();

        _actorSystem = ActorSystem.Create("GameSystem");
        // instance of actor system

        ActorReferences.GameController = _actorSystem.ActorOf<GameControllerActor>();
        // reference to player actors in the system. it is a good practice to keep the reference to actors in a dedicated class

        ActorReferences.SignalRBridge = _actorSystem.ActorOf(
            Props.Create(
                () => new SignalRBridgeActor(_gameEventsPusher, ActorReferences.GameController)
            ), "SignalRBridge"
        );
    }

    public static void Shutdown()
    {
        _actorSystem.Terminate().Wait();
    }

    public static class ActorReferences
    {
        public static IActorRef GameController { get; set; }
        public static IActorRef SignalRBridge { get; set; }
    }
}
