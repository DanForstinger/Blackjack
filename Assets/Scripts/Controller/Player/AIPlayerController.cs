using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : PlayerController
{
    //todo: Put constants somewhere?
    private const int maxScoreToHit = 17;
    
    protected override void BeginTurn()
    {
        if (Model.Score < maxScoreToHit)
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
