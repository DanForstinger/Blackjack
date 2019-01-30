[System.Serializable]
public class DeclareWinnerAction : GameAction
{
    public PlayerModel Winner { get; private set; }
    public PlayerModel Loser { get; private set; }
    public DeclareWinnerAction(PlayerModel winner, PlayerModel loser)
    {
        Winner = winner;
        Loser = loser;
    }
}
