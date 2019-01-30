using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawCardViewer : PlayerActionViewer
{
    [SerializeField] private ObjectPool cardPool;
    [SerializeField] private Transform deckTransform;

    private const float moveTime = 0.2f;
    private const float scaleTime = 0.1f;
    private const float cardRotationOffset = 8f;
    
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

        LeanTween.move(cardView.gameObject, transform.position, moveTime)
            .setOnComplete(() =>
            {
                cardView.transform.SetParent(transform);
                cardView.transform.SetAsLastSibling();

                FanCards();

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

    private void FanCards()
    {
        int cardCount = transform.childCount;
        float maxRotation = cardRotationOffset * (float)cardCount;
        
        for (int i = 0; i < cardCount; ++i)
        {
            float rotation = (i * -cardRotationOffset) + maxRotation/2;
            rotation = rotation < 0 ? 360 - Mathf.Abs(rotation) : rotation;
            transform.GetChild(i).eulerAngles = new Vector3(0,0, rotation);
        }
    }
}
