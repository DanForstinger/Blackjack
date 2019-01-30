using UnityEngine;

[System.Serializable]
public class PlaceBetAction : GameAction, IPlayerAction
{
    public int Value { get; private set; }
    
    public PlayerModel Player { get; set; }

    public PlaceBetAction(int value, PlayerModel player)
    {
        Value = value;
        Player = player;
    }
}
