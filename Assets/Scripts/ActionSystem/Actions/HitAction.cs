[System.Serializable]
public class HitAction : GameAction, IPlayerAction
{
    public PlayerModel Player { get; set; }
    
    public HitAction(PlayerModel player)
    {
        Player = player;
    }
}
