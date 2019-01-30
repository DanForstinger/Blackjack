using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameModel
{ 
    public PlayerModel[] Players;
    public DeckModel Deck;
    public GameResult Result = GameResult.Undeclared;
    public int CurrentPlayerTurn = 0;

    
    public int Winner;
    
    public GameModel(PlayerModel[] players)
    {
        Players = players;
        Deck = new DeckModel();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
