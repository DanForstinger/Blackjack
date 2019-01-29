using UnityEngine;

[System.Serializable]
public class CardModel
{
    public Suit Suit;
    public int Rank;

    public CardModel(Suit suit, int rank)
    {
        Suit = suit;
        Rank = rank;
    }
}
