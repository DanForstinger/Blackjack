[System.Serializable]
public class DeclareGameResultAction : GameAction
{
    public PlayerModel LocalPlayer { get; private set; }
    public PlayerModel Dealer { get; private set; }
    
    public GameResult Result { get; private set; }
    
    public DeclareGameResultAction(PlayerModel localPlayer, PlayerModel dealer, GameResult result)
    {
        LocalPlayer = localPlayer;
        Dealer = dealer;
        Result = result;
    }
}
