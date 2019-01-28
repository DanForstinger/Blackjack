[System.Serializable]
public class UpdateScoreAction : GameAction, IPlayerAction
{
    public int Score { get; private set; }
    public int OwningPlayer { get; set; }
    
    public UpdateScoreAction(int score, int playerIndex)
    {
        Score = score;
        OwningPlayer = playerIndex;
    }
}
