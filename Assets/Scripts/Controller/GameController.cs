using System.Linq;
using UnityEngine;

public enum GameResult
{
    Undeclared,
    PlayerWins,
    DealerWins,
    Tie
}

//todo: split into smaller components?
public class GameController : MonoBehaviour
{
    public GameModel Model { get; private set; }

    [SerializeField] private IntRangeValue startingMoney;
    [SerializeField] private IntRangeValue minimumBet;
    
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
        
        playerModels[0] = player.Initialize(0, true, startingMoney.Value);
        playerModels[1] = dealer.Initialize(1, false, startingMoney.Value);

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
        
        var betAction = new AddBetValueAction(minimumBet.Value, player.Model.PlayerIndex);
        ActionSystem.Instance.PerformAction(betAction);
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
            EndGame();
        }
        else
        {
            var changeTurnAction = new BeginTurnAction(nextTurn);
            ActionSystem.Instance.PerformAction(changeTurnAction);
        }
    }

    private void EndGame()
    {
        var gameOverAction = new EndGameAction();
        ActionSystem.Instance.PerformAction(gameOverAction);

        var result = CalculateResult();

        var declareResultAction = new DeclareGameResultAction(player.Model, dealer.Model, result);
        ActionSystem.Instance.PerformAction(declareResultAction);
    }

    GameResult CalculateResult()
    {
        if (!player.Model.DidBust && (player.Model.Score > dealer.Model.Score || dealer.Model.DidBust))
        {
            return GameResult.PlayerWins;
        }
        else if ((player.Model.DidBust && dealer.Model.DidBust) || player.Model.Score == dealer.Model.Score)
        {
            return GameResult.Tie;
        }
        else
        {
            return GameResult.DealerWins;
        }
    }
    
    bool IsGameOver()
    {
        return Model.Players.All(player => player.DidStay || player.DidBust);
    }
}
