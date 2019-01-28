using UnityEngine;

public class GameController : MonoBehaviour
{
    public DeckModel Deck { get; private set; }
    
    [SerializeField] private PlayerController[] players;

    void Awake()
    {
        Deck = new DeckModel();

        for (int i = 0; i < players.Length; ++i)
        {
            players[i].Initialize(i);
        }
    }

    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<HitAction>(OnHitAction);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<HitAction>(OnHitAction);    
    }
    
    void Start()
    {
        DrawCard(0, true);
        DrawCard(0, true);
        DrawCard(1, false);
        DrawCard(1, true);
    }

    void OnHitAction(GameAction action)
    {
        var hitAction = (HitAction) action;
        DrawCard(hitAction.OwningPlayer, true);
    }

    void DrawCard(int drawingPlayerIndex, bool shouldReveal)
    {
        var drawCardAction = new DrawCardAction(Deck.DrawCard(), drawingPlayerIndex, shouldReveal);
        ActionSystem.Instance.PerformAction(drawCardAction);
    }
}
