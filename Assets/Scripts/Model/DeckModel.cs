using System;
using System.Collections.Generic;

[System.Serializable]
public class DeckModel
{
    private List<CardModel> cards;

    private int currentDeckIndex;
    
    public DeckModel()
    {
        cards = new List<CardModel>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int rank = 1; rank < 13; ++rank)
            {
                cards.Add(new CardModel(suit, rank));
            }
        }

        ShuffleDeck();
    }

    public CardModel DrawCard()
    {
        var card = cards[currentDeckIndex];
        
        currentDeckIndex++;
        if (currentDeckIndex >= cards.Count)
        {
            ShuffleDeck();
        }

        return card;
    }

    public void ShuffleDeck()
    {
        cards.Shuffle();
        currentDeckIndex = 0;
    }
}
