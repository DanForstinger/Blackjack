using UnityEngine;

[System.Serializable]
public class AdjustPlayerMoneyAction : GameAction, IPlayerAction
{
    public int NewValue { get; private set; }
    
    public int AdjustmentAmount { get; private set; }
    
    public int OwningPlayer { get; set; }

    public AdjustPlayerMoneyAction(int amount, int newValue, int playerIndex)
    {
        AdjustmentAmount = amount;
        NewValue = newValue;
        OwningPlayer = playerIndex;
    }
}
