using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : PlayerController
{
    //todo: Put constants somewhere?
    [SerializeField] private IntRangeValue maxHitValue;
    protected override void BeginTurn()
    {
        if (Model.Score < maxHitValue.Value) 
        {
            var hitAction = new HitAction(Model.PlayerIndex);
            ActionSystem.Instance.PerformAction(hitAction);
        }
        else
        {
            var stayAction = new StayAction(Model.PlayerIndex);
            ActionSystem.Instance.PerformAction(stayAction);
        }
    }
}
