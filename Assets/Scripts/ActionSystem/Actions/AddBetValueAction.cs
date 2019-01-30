using UnityEngine;

[System.Serializable]
public class AddBetValueAction : GameAction, IPlayerAction
{
    public int Value { get; private set; }
    
    public int OwningPlayer { get; set; }

    public AddBetValueAction(int value, int playerIndex)
    {
        Value = value;
        OwningPlayer = playerIndex;
    }
}
