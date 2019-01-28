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
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StayAction>(OnStayAction);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<HitAction>(OnHitAction);    
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StayAction>(OnStayAction);   
    }

    void Start()
    {
        StartGame();
    }
    
    void StartGame()
    {
        DrawCard(0, true);
        DrawCard(0, true);
        
        DrawCard(1, false);
        DrawCard(1, true);
        
        ChangeTurn(1);
    }
    
    void DrawCard(int drawingPlayerIndex, bool shouldReveal)
    {
        //todo: should all player actions require an index, or a controller?
        var drawCardAction = new DrawCardAction(Deck.DrawCard(), drawingPlayerIndex, shouldReveal);
        ActionSystem.Instance.PerformAction(drawCardAction);
    }
    
    void OnHitAction(GameAction action)
    {
        var hitAction = (HitAction) action;

        DrawCard(hitAction.OwningPlayer, true);
       
        ChangeTurn(hitAction.OwningPlayer);
    }

    void OnStayAction(GameAction action)
    {
        var stayAction = (StayAction) action;

        ChangeTurn(stayAction.OwningPlayer);
    }
    
    void ChangeTurn(int currentTurn)
    {
        int nextTurn = currentTurn == 0 ? 1 : 0;
        
        var changeTurnAction = new BeginTurnAction(nextTurn);
        ActionSystem.Instance.PerformAction(changeTurnAction);
    }
}
