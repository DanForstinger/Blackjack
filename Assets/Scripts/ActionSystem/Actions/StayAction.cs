[System.Serializable]
public class StayAction : GameAction, IPlayerAction
{
    public PlayerModel Player { get; set; }
    
    public StayAction(PlayerModel player)
    {
        Player = player;
    }
}
