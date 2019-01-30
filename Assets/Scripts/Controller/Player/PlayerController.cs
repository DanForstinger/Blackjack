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

    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<AddBetValueAction>(OnAddBet);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DeclareGameResultAction>(OnDeclareWinner);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<PlaceBetAction>(OnPlaceBet);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<AddBetValueAction>(OnAddBet);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DeclareGameResultAction>(OnDeclareWinner);
    }

    void OnDrawCard(GameAction action)
    {
        var drawCardAction = (DrawCardAction)action;

        if (drawCardAction.OwningPlayer == Model.PlayerIndex)
        {
            Model.AddCard(drawCardAction.Card);
            
            //todo: Maybe this shouldn't be an action...
            var updateAction = new UpdateScoreAction(Model.Score, Model.PlayerIndex);
            ActionSystem.Instance.PerformAction(updateAction);
        }
    }

    void OnBeginTurn(GameAction action)
    {
        var beginTurnAction = (BeginTurnAction) action;

        if (beginTurnAction.OwningPlayer == Model.PlayerIndex)
        {
            BeginTurn();
        }
    }

    void OnAddBet(GameAction action)
    {
        var betAction = (AddBetValueAction) action;

        if (betAction.OwningPlayer == Model.PlayerIndex)
        {
            AdjustMoney(-betAction.Value);
        }
    }
    
    void OnPlaceBet(GameAction action)
    {
        var betAction = (PlaceBetAction) action;

        if (betAction.OwningPlayer == Model.PlayerIndex)
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
                AdjustMoney(Model.Bet * 2);
            }
            else if (resultAction.Result == GameResult.Tie)
            {
                AdjustMoney(Model.Bet);
            }
        }
    }

    void AdjustMoney(int amount)
    {
        Model.Money += amount;
        
        if (Model.Money <= 0) Model.Money = 0;
        
        var adjustAction = new AdjustPlayerMoneyAction(amount, Model.Money, Model.PlayerIndex);
        ActionSystem.Instance.PerformAction(adjustAction);
    }
    
    protected abstract void BeginTurn();
}
