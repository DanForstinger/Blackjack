using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckModel
{
    public const int CardsPerSuit = 13;
    public const int DeckCount = 8;
    
    private List<CardModel> cards;

    private int currentDeckIndex;

    public DeckModel()
    {
        cards = new List<CardModel>();

        for (int i = 0; i < DeckCount; ++i)
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int rank = 1; rank <= CardsPerSuit; ++rank)
                {
                    cards.Add(new CardModel(suit, rank));
                }
            }
        }

        Debug.Log(string.Format("Created a deck with {0} cards.", cards.Count));
        
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
