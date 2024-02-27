namespace GameApp.ActorModel.Messages;

public class AttackPlayerMessage
{
    public string PlayerName { get; private set; }
    
    public AttackPlayerMessage(string playerName)
    {
        PlayerName = playerName;
    }
}
