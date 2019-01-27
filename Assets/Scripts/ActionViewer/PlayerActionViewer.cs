using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerActionViewer : ActionViewer
{
    protected PlayerController playerController { get; private set; }

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public override bool WillViewAction(GameAction action)
    {
        var playerAction = action as IPlayerAction;
        return playerAction.OwningPlayer == playerController.playerModel.PlayerIndex;
    }
}