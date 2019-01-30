using UnityEngine.UI;
using UnityEngine;

public class PlaceBetButtonController : ButtonController
{
    [SerializeField] private BetPlacementController betPlacementController;
    [SerializeField] private PlayerController playerController;

    protected override void OnButtonClicked()
    {
        var action = new PlaceBetAction(betPlacementController.CurrentBet, playerController.Model);
        ActionSystem.Instance.PerformAction(action);
    }
}
