[System.Serializable]
public class StayAction : GameAction, IPlayerAction
{
    public int OwningPlayer { get; set; }
    
    public StayAction(int playerIndex)
    {
        OwningPlayer = playerIndex;
    }
}
