[System.Serializable]
public class UpdateScoreAction : GameAction, IPlayerAction
{
    public int Score { get; private set; }
    public PlayerModel Player { get; set; }
    
    public UpdateScoreAction(int score, PlayerModel player)
    {
        Score = score;
        Player = player;
    }
}
