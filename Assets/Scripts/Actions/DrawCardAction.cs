[System.Serializable]
public class DrawCardAction : GameAction, IPlayerAction
{
    public CardModel Card { get; private set; }

    public int OwningPlayer { get; set; }
    
    public DrawCardAction(CardModel card, int playerIndex)
    {
        Card = card;
        OwningPlayer = playerIndex;
    }
}
