using System.Linq;
using UnityEngine;

//todo: split into smaller components?
public class GameController : MonoBehaviour
{
    public GameModel Model { get; private set; }

    public IntRangeValue StartingMoney;
  
    [SerializeField] private AIPlayerController dealer;

    //To support multiple players, make this an array of players.
    [SerializeField] private InputPlayerController player;
    
    //todo; Seperate class?
    [SerializeField] private GameObject BetPlacementUI;
    [SerializeField] private GameObject GameplayControlsUI;
    
    private int currentPlayer = 0;

    void Awake()
    {
        var playerModels = new PlayerModel[2];
        
        playerModels[0] = player.Initialize(0, true, StartingMoney.Value);
        playerModels[1] = dealer.Initialize(1, false, StartingMoney.Value);

        Model = new GameModel(playerModels);
    }

    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<HitAction>(OnHitAction);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StayAction>(OnStayAction);
    }

    void OnDisable()
    {
        //todo: Rename to event system?
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<HitAction>(OnHitAction);    
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StayAction>(OnStayAction);   
    }

    void OnStartGame(GameAction action)
    {
        Model.Deck.ShuffleDeck();
        
        BetPlacementUI.SetActive(true);
        GameplayControlsUI.SetActive(false);
    }
    
    void OnPlaceBet(GameAction action)
    {
        BetPlacementUI.SetActive(false);
        GameplayControlsUI.SetActive(true);
        
        DealCards();

        ChangeTurn(1);
    }

    private void DealCards()
    {
        DrawCard(0, true);
        DrawCard(0, true);

        DrawCard(1, false);
        DrawCard(1, true);
    }

    void DrawCard(int drawingPlayerIndex, bool shouldReveal)
    {
        //todo: should all player actions require an index, or a controller?
        var drawCardAction = new DrawCardAction(Model.Deck.DrawCard(), drawingPlayerIndex, shouldReveal);
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

        Model.Players[stayAction.OwningPlayer].DidStay = true;
        
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

            var winner = CalculateWinner();

            var declareWinnerAction = new DeclareWinnerAction(Model.Players[winner], Model.Players[1-winner]);
            ActionSystem.Instance.PerformAction(declareWinnerAction);
        }
        else
        {
            var changeTurnAction = new BeginTurnAction(nextTurn);
            ActionSystem.Instance.PerformAction(changeTurnAction);
        }
    }

    int CalculateWinner()
    {
        if (!player.Model.DidBust && (player.Model.Score > dealer.Model.Score || dealer.Model.DidBust))
        {
            return player.Model.PlayerIndex;
        }
        else
        {
            return dealer.Model.PlayerIndex;
        }
    }
    
    bool IsGameOver()
    {
        return Model.Players.All(player => player.DidStay || player.DidBust);
    }
}
