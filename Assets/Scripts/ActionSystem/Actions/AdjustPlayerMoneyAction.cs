using UnityEngine;

[System.Serializable]
public class AdjustPlayerMoneyAction : GameAction, IPlayerAction
{
    public int NewValue { get; private set; }
    
    public int AdjustmentAmount { get; private set; }
    
    public PlayerModel Player { get; set; }

    public AdjustPlayerMoneyAction(int amount, int newValue, PlayerModel player)
    {
        AdjustmentAmount = amount;
        NewValue = newValue;
        Player = player;
    }
}
