using UnityEngine;

[System.Serializable]
public class AddBetValueAction : GameAction
{
    public Sprite Chip { get; private set; }
    public int Value { get; private set; }
    
    public int OwningPlayer { get; set; }

    public AddBetValueAction(int value, Sprite chip, int playerIndex)
    {
        Value = value;
        Chip = chip;
        OwningPlayer = playerIndex;
    }
}
