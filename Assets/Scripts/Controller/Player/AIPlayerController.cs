using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : PlayerController
{
    protected override void BeginTurn()
    {
        var hitAction = new HitAction(Model.PlayerIndex);
        ActionSystem.Instance.PerformAction(hitAction);
    }
}
