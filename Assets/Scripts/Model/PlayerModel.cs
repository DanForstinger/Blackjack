using System.Collections.Generic;
using UnityEngine;

//todo: Should the hand be its own model?
public class PlayerModel
{
    public int PlayerIndex;

    public List<CardModel> Cards;

    public int Score { get; private set; }
    
    public PlayerModel(int index)
    {
        Cards = new List<CardModel>();
        PlayerIndex = index;
        Score = 0;
    }
    
    public void AddCard(CardModel card)
    {
        Cards.Add(card);

        Score = CalculateHandTotal();
        
        Debug.Log(string.Format("Player {0} received a {1} of {2}'s. Their score is {3}", PlayerIndex, card.Rank, card.Suit.ToString(), Score));
    }

    private int CalculateHandTotal()
    {
        int total = 0;
        int numberOfAces = 0;
        
        foreach (var card in Cards)
        {
            int rank = card.Rank; //convert from 0 based to 1 based

            if (rank == 1) //track the aces
            {
                total += 11;
                numberOfAces++;
            }
            else if (rank <= 10) //normal numeric value
            {
                total += rank;
            }
            else //face cards.
            {
                total += 10;
            }
        }

        while (IsBust(total) && numberOfAces > 0)
        {
            total -= 10;
        }
        
        return total;
    }

    private bool IsBust(int score)
    {
        return score > 21;
    }
}
