using System.Linq;
using UnityEngine;

//todo: split into smaller components?
public class GameController : MonoBehaviour
{
    public DeckModel Deck { get; private set; }
    
    [SerializeField] private PlayerController[] players;

    private int currentPlayer = 0;
    
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
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<HitAction>(OnHitAction);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StayAction>(OnStayAction);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<HitAction>(OnHitAction);    
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StayAction>(OnStayAction);   
    }
    
    void OnStartGame(GameAction action)
    {
        DrawCard(0, true);
        DrawCard(0, true);
        
        DrawCard(1, true);
        DrawCard(1, false);
        
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

        if (currentPlayer == hitAction.OwningPlayer)
        {
            DrawCard(hitAction.OwningPlayer, true);

            ChangeTurn(hitAction.OwningPlayer);
        }
    }

    void OnStayAction(GameAction action)
    {
        var stayAction = (StayAction) action;
        if (currentPlayer == stayAction.OwningPlayer)
        {
            ChangeTurn(stayAction.OwningPlayer);
        }
    }
    
    void ChangeTurn(int currentTurn)
    {
        int nextTurn = currentTurn == 0 ? 1 : 0;

        currentPlayer = nextTurn;

        if (IsGameOver())
        {
            var gameOverAction = new EndGameAction();
            ActionSystem.Instance.PerformAction(gameOverAction);
        }
        else
        {
            var changeTurnAction = new BeginTurnAction(nextTurn);
            ActionSystem.Instance.PerformAction(changeTurnAction);
        }
    }

    bool IsGameOver()
    {
        return players.All(player => player.Model.DidStay || player.Model.DidBust);
    }
}
