using UnityEngine;

[System.Serializable]
public class AddBetValueAction : GameAction, IPlayerAction
{
    public int Value { get; private set; }
    
    public PlayerModel Player { get; set; }

    public AddBetValueAction(int value, PlayerModel player)
    {
        Value = value;
        Player = player;
    }
}
