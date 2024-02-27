using Akka.Actor;
using GameApp.ActorModel.Actors;

namespace Game.Web.Models;

public static class GameActorSystem
{
    private static ActorSystem _actorSystem;

    public static void Create()
    {
        _actorSystem = ActorSystem.Create("GameSystem");
        // instance of actor system

        ActorReferences.GameController = _actorSystem.ActorOf<GameControllerActor>(); 
        // reference to player actors in the system. it is a good practice to keep the reference to actors in a dedicated class
    }
    
    public static void Shutdown()
    {
        _actorSystem.Terminate().Wait();
    }

    public static class ActorReferences
    {
        public static IActorRef GameController { get; set; }
    }
}
