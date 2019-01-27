[System.Serializable]
public class DrawCardAction : GameAction
{
    public CardModel Card { get; private set; }

    public int OwningPlayer { get; private set; }
    
    public DrawCardAction(CardModel card, int playerIndex)
    {
        Card = card;
        OwningPlayer = playerIndex;
    }
}
