using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerModel
{
    public int PlayerIndex;

    public int Bet = 0;
    
    public int Money;
    
    public bool DidStay = false;
    
    public bool DidBust
    {
        get { return IsBust(Score); }
    }

    public bool IsLocalPlayer; 
    
    public int Score;

    public List<CardModel> Hand;

    public PlayerModel(int index, bool isLocalPlayer, int startingMoney)
    {
        IsLocalPlayer = isLocalPlayer;
        Hand = new List<CardModel>();
        PlayerIndex = index;
        Score = 0;
        Money = startingMoney;
    }
    
    public void AddCard(CardModel card)
    {
        Hand.Add(card);

        Score = CalculateHandTotal();
        
        Debug.Log(string.Format("Player {0} received a {1} of {2}'s. Their score is {3}", PlayerIndex, card.Rank, card.Suit.ToString(), Score));
    }

    private int CalculateHandTotal()
    {
        int total = 0;
        int numberOfAces = 0;
        
        foreach (var card in Hand)
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
            numberOfAces--;
        }
        
        return total;
    }

    private bool IsBust(int score)
    {
        return score > 21;
    }
}
