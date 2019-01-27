using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawCardViewer : PlayerActionViewer
{
    void OnEnable()
    {
        RegisterViewer<DrawCardAction>();
    }

    void OnDisable()
    {
        UnregisterViewer<DrawCardAction>();
    }

    public override void ExecuteViewAction(GameAction action, UnityAction<ActionViewer> onCompleteCallback)
    {
        var drawCardAction = (DrawCardAction) action;

        if (drawCardAction.OwningPlayer == playerController.playerModel.PlayerIndex)
        {
            StartCoroutine(AnimateDrawCard(drawCardAction, onCompleteCallback));
        }
    }

    IEnumerator AnimateDrawCard(DrawCardAction action, UnityAction<ActionViewer> onCompleteCallback)
    {
        var wait = new WaitForSeconds(2);
        
        yield return wait;
        
        onCompleteCallback.Invoke(this);
    }
}
