using UnityEngine.Events;
using UnityEngine;

public abstract class PlayerActionViewer : ActionViewer
{
    [SerializeField] protected PlayerController playerController;

    public override bool WillViewAction(GameAction action)
    {
        var playerAction = action as IPlayerAction;
        return playerAction.OwningPlayer == playerController.Model.PlayerIndex;
    }
}