[System.Serializable]
public class DrawCardAction : GameAction, IPlayerAction
{
    public CardModel Card { get; private set; }

    public PlayerModel Player { get; set; }
    
    public bool ShouldReveal { get; set; }
    
    public DrawCardAction(CardModel card, PlayerModel player, bool shouldReveal)
    {
        Card = card;
        Player = player;
        ShouldReveal = shouldReveal;
    }
}
