[System.Serializable]
public class DrawCardAction : GameAction, IPlayerAction
{
    public CardModel Card { get; private set; }

    public int OwningPlayer { get; set; }
    
    public bool ShouldReveal { get; set; }
    
    public DrawCardAction(CardModel card, int playerIndex, bool shouldReveal)
    {
        Card = card;
        OwningPlayer = playerIndex;
        ShouldReveal = shouldReveal;
    }
}
