using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawCardViewer : PlayerActionViewer
{
    [SerializeField] private ObjectPool cardPool;
    [SerializeField] private Transform handTransform;
    
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

        StartCoroutine(AnimateDrawCard(drawCardAction, onCompleteCallback));
    }

    IEnumerator AnimateDrawCard(DrawCardAction action, UnityAction<ActionViewer> onCompleteCallback)
    {
        var card = cardPool.GetObject();
        card.transform.SetParent(handTransform);
        
        var wait = new WaitForSeconds(2);
        
        yield return wait;
        
        onCompleteCallback.Invoke(this);
    }
}
