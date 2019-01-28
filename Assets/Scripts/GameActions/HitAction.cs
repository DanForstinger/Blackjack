[System.Serializable]
public class HitAction : GameAction, IPlayerAction
{
    public int OwningPlayer { get; set; }
    
    public HitAction(int playerIndex)
    {
        OwningPlayer = playerIndex;
    }
}
