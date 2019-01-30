using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : PlayerController
{
    [SerializeField] private IntRangeValue maxHitValue;
    
    protected override void BeginTurn()
    {
        if (Model.Score < maxHitValue.Value) 
        {
            var hitAction = new HitAction(Model);
            ActionSystem.Instance.PerformAction(hitAction);
        }
        else
        {
            var stayAction = new StayAction(Model);
            ActionSystem.Instance.PerformAction(stayAction);
        }
    }
}
