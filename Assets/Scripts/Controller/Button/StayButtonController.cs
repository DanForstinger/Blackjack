using UnityEngine;

public class StayButtonController : ButtonController
{
    [SerializeField] private PlayerController playerController;
    
    protected override void OnButtonClicked()
    {
        var stayAction = new StayAction(playerController.Model.PlayerIndex);
        ActionSystem.Instance.PerformAction(stayAction);
    }
}
