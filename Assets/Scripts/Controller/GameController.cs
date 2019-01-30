using System.Linq;
using UnityEngine;

public enum GameResult
{
    Undeclared,
    PlayerWins,
    DealerWins,
    Tie
}

public class GameController : MonoBehaviour
{
    public AIPlayerController Dealer;

    //To support multiple players, make this an array of players.
    public InputPlayerController LocalPlayer;
    
    public GameModel Model { get; private set; }

    [SerializeField] private IntRangeValue startingMoney;
    [SerializeField] private IntRangeValue minimumBet;
    
    private int currentPlayer = 0;

    void Awake()
    {
        var playerModels = new PlayerModel[2];
        
        playerModels[0] = LocalPlayer.Initialize(0, true, startingMoney.Value);
        playerModels[1] = Dealer.Initialize(1, false, startingMoney.Value);

        Model = new GameModel(playerModels);
    }

    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.Listeners.AddListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.Listeners.AddListener<HitAction>(OnHitAction);
        ActionSystem.Instance.Listeners.AddListener<StayAction>(OnStayAction);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.Listeners.RemoveListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.Listeners.RemoveListener<HitAction>(OnHitAction);    
        ActionSystem.Instance.Listeners.RemoveListener<StayAction>(OnStayAction);   
    }

    void OnStartGame(GameAction action)
    {
        Model.Deck.ShuffleDeck();
        
        var betAction = new AddBetValueAction(minimumBet.Value, LocalPlayer.Model);
        ActionSystem.Instance.PerformAction(betAction);
    }
    
    void OnPlaceBet(GameAction action)
    {
        DealCards();

        ChangeTurn(1);
    }

    private void DealCards()
    {
        DrawCard(LocalPlayer.Model, true);
        DrawCard(LocalPlayer.Model, true);

        DrawCard(Dealer.Model, false);
        DrawCard(Dealer.Model, true);
    }

    void DrawCard(PlayerModel player, bool shouldReveal)
    {
        var drawCardAction = new DrawCardAction(Model.Deck.DrawCard(), player, shouldReveal);
        ActionSystem.Instance.PerformAction(drawCardAction);
    }
    
    void OnHitAction(GameAction action)
    {
        var hitAction = (HitAction) action;

        if (currentPlayer == hitAction.Player.PlayerIndex)
        {
            DrawCard(hitAction.Player, true);

            ChangeTurn(hitAction.Player.PlayerIndex);
        }
    }

    void OnStayAction(GameAction action)
    {
        var stayAction = (StayAction) action;

        Model.Players[stayAction.Player.PlayerIndex].DidStay = true;
        
        if (currentPlayer == stayAction.Player.PlayerIndex)
        {
            ChangeTurn(stayAction.Player.PlayerIndex);
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
            var changeTurnAction = new BeginTurnAction(Model.Players[nextTurn]);
            ActionSystem.Instance.PerformAction(changeTurnAction);
        }
    }

    private void EndGame()
    {
        var gameOverAction = new EndGameAction();
        ActionSystem.Instance.PerformAction(gameOverAction);

        var result = CalculateResult();

        var declareResultAction = new DeclareGameResultAction(LocalPlayer.Model, Dealer.Model, result);
        ActionSystem.Instance.PerformAction(declareResultAction);
    }

    GameResult CalculateResult()
    {
        if (!LocalPlayer.Model.DidBust && (LocalPlayer.Model.Score > Dealer.Model.Score || Dealer.Model.DidBust))
        {
            return GameResult.PlayerWins;
        }
        else if ((LocalPlayer.Model.DidBust && Dealer.Model.DidBust) || LocalPlayer.Model.Score == Dealer.Model.Score)
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
