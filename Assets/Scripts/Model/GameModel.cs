using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameModel
{ 
    public PlayerModel[] Players;
    public DeckModel Deck;

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
