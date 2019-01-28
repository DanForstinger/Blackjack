using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawCardViewer : PlayerActionViewer
{
    [SerializeField] private ObjectPool cardPool;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform deckTransform;

    private const float moveTime = 0.2f;
    private const float scaleTime = 0.1f;
    
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
        
        var cardView = SpawnCard(drawCardAction.Card);

        LeanTween.move(cardView.gameObject, handTransform.position, moveTime)
            .setOnComplete(() =>
            {
                cardView.transform.SetParent(handTransform);
                cardView.transform.SetAsLastSibling();

                if (drawCardAction.ShouldReveal)
                {
                    LeanTween.scaleX(cardView.gameObject, 0, scaleTime)
                        .setOnComplete(() =>
                        {
                            cardView.SetRevealed(true);
                            LeanTween.scaleX(cardView.gameObject, 1, scaleTime)
                                .setOnComplete(() => onCompleteCallback.Invoke(this));
                        });
                }
                else
                {
                    onCompleteCallback.Invoke(this);
                }
            });
    }

    private CardView SpawnCard(CardModel card)
    {
        var cardObj = cardPool.GetObject();
        cardObj.transform.SetParent(deckTransform);
        cardObj.transform.localPosition = Vector2.zero;

        var cardView = cardObj.GetComponent<CardView>();
        cardView.SetCard(card, false);
        
        return cardView;
    }
}
