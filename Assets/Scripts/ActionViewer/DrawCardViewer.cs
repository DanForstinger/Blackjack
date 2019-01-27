using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawCardViewer : PlayerActionViewer
{
    [SerializeField] private ObjectPool cardPool;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform deckTransform;

    private const float tweenSpeed = 0.2f;
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

        var card = cardPool.GetObject();
        
        card.transform.SetParent(deckTransform);
        card.transform.localPosition = Vector2.zero;

        LeanTween.move(card, handTransform.position, tweenSpeed)
            .setOnComplete(() =>
            {
                card.transform.SetParent(handTransform);
                card.transform.SetAsLastSibling();
                onCompleteCallback.Invoke(this);
            });
    }
}
