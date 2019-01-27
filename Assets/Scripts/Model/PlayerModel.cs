using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int PlayerIndex;

    public List<CardModel> Cards;

    public PlayerModel(int index)
    {
        Cards = new List<CardModel>();
        PlayerIndex = index;
    }
    
    public void AddCard(CardModel card)
    {
        Cards.Add(card);
        
        Debug.Log(string.Format("Player {0} received a {1} of {2}'s", PlayerIndex, card.Rank, card.Suit.ToString()));
    } 
}
