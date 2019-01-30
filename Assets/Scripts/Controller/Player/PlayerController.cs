using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public PlayerModel Model { get; private set; }

    public PlayerModel Initialize(int index, bool isLocal, int startingMoney)
    {
        Model = new PlayerModel(index, isLocal, startingMoney);
        
        Debug.Log(string.Format("Initialized a player controller at index {0}.", index));

        return Model;
    }

    public void SetMoney(int amount)
    {
        Model.Money = amount;
        
        if (Model.Money <= 0) Model.Money = 0;
        
        var adjustAction = new AdjustPlayerMoneyAction(amount, Model.Money, Model);
        ActionSystem.Instance.PerformAction(adjustAction);
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.Listeners.AddListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.Listeners.AddListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.Listeners.AddListener<AddBetValueAction>(OnAddBet);
        ActionSystem.Instance.Listeners.AddListener<DeclareGameResultAction>(OnDeclareWinner);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.Listeners.RemoveListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.Listeners.RemoveListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.Listeners.RemoveListener<AddBetValueAction>(OnAddBet);
        ActionSystem.Instance.Listeners.RemoveListener<DeclareGameResultAction>(OnDeclareWinner);
    }

    void OnDrawCard(GameAction action)
    {
        var drawCardAction = (DrawCardAction)action;

        if (drawCardAction.Player.PlayerIndex == Model.PlayerIndex)
        {
            Model.AddCard(drawCardAction.Card);
            
            var updateAction = new UpdateScoreAction(Model.Score, Model);
            ActionSystem.Instance.PerformAction(updateAction);
        }
    }

    void OnBeginTurn(GameAction action)
    {
        var beginTurnAction = (BeginTurnAction) action;

        if (beginTurnAction.Player.PlayerIndex == Model.PlayerIndex)
        {
            BeginTurn();
        }
    }

    void OnAddBet(GameAction action)
    {
        var betAction = (AddBetValueAction) action;

        if (betAction.Player.PlayerIndex == Model.PlayerIndex)
        {
            SetMoney(Model.Money - betAction.Value);
        }
    }
    
    void OnPlaceBet(GameAction action)
    {
        var betAction = (PlaceBetAction) action;

        if (betAction.Player.PlayerIndex == Model.PlayerIndex)
        {
            Model.Bet = betAction.Value;
        }
    }
    
    void OnDeclareWinner(GameAction action)
    {
        var resultAction = (DeclareGameResultAction) action;

        if (Model.IsLocalPlayer)
        {
            if (resultAction.Result == GameResult.PlayerWins)
            {
                SetMoney(Model.Money + (Model.Bet * 2));
            }
            else if (resultAction.Result == GameResult.Tie)
            {
                SetMoney(Model.Money + Model.Bet);
            }
        }
    }
    
    protected abstract void BeginTurn();
}
