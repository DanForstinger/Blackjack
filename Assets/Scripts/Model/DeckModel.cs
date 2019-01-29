using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//todo: Actually remove cards from the deck instead of using this int.
[System.Serializable]
public class DeckModel
{
    public const int CardsPerSuit = 13;
    public const int DeckCount = 8;
    public List<CardModel> Cards;
    public int CurrentDeckIndex;

    public DeckModel()
    {
        Cards = new List<CardModel>();

        for (int i = 0; i < DeckCount; ++i)
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int rank = 1; rank <= CardsPerSuit; ++rank)
                {
                    Cards.Add(new CardModel(suit, rank));
                }
            }
        }

        Debug.Log(string.Format("Created a deck with {0} cards.", Cards.Count));
        
        ShuffleDeck();
    }

    public CardModel DrawCard()
    {
        var card = Cards[CurrentDeckIndex];
        
        CurrentDeckIndex++;
        if (CurrentDeckIndex >= Cards.Count)
        {
            ShuffleDeck();
        }

        return card;
    }

    public void ShuffleDeck()
    {
        Cards.Shuffle();
        CurrentDeckIndex = 0;
    }
}
