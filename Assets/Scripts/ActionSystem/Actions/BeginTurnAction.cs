[System.Serializable]
public class BeginTurnAction : GameAction, IPlayerAction
{
    public int OwningPlayer { get; set; }
    
    public BeginTurnAction(int playerIndex)
    {
        OwningPlayer = playerIndex;
    }
}
