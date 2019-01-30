[System.Serializable]
public class BeginTurnAction : GameAction, IPlayerAction
{
    public PlayerModel Player { get; set; }
    
    public BeginTurnAction(PlayerModel player)
    {
        Player = player;
    }
}
