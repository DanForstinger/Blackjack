using UnityEngine;

public class InputPlayerController : PlayerController
{
    protected override void BeginTurn()
    {
        if (Model.DidStay || Model.DidBust)
        {
            var stayAction = new StayAction(Model);
            ActionSystem.Instance.PerformAction(stayAction);
        }
    }
}
