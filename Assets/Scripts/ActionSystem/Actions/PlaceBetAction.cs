using UnityEngine;

[System.Serializable]
public class PlaceBetAction : GameAction, IPlayerAction
{
    public int Value { get; private set; }
    
    public int OwningPlayer { get; set; }

    public PlaceBetAction(int value, int playerIndex)
    {
        Value = value;
        OwningPlayer = playerIndex;
    }
}
