using UnityEngine;

//todo: split into smaller components?
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
    
    void DrawCard(int drawingPlayerIndex, bool shouldReveal)
    {
        var drawCardAction = new DrawCardAction(Deck.DrawCard(), drawingPlayerIndex, shouldReveal);
        ActionSystem.Instance.PerformAction(drawCardAction);
    }
    
    void OnHitAction(GameAction action)
    {
        var hitAction = (HitAction) action;

        DrawCard(hitAction.OwningPlayer, true);
        
        int nextTurn = hitAction.OwningPlayer == 0 ? 1 : 0;

        ChangeTurn(nextTurn);
    }

    void ChangeTurn(int newTurn)
    {
        var changeTurnAction = new BeginTurnAction(newTurn);
        ActionSystem.Instance.PerformAction(changeTurnAction);
    }
}
