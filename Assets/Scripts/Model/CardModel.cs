using UnityEngine;

[System.Serializable]
public class CardModel
{
    public Suit Suit { get; private set; }
    public int Rank { get; private set; }

    public CardModel(Suit suit, int rank)
    {
        Suit = suit;
        Rank = rank;
    }
}
