﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public PlayerModel Model { get; private set; }

    public void Initialize(int index)
    {
        Model = new PlayerModel(index);
        
        Debug.Log(string.Format("Initialized a player controller at index {0}.", index));
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<BeginTurnAction>(OnBeginTurn);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DrawCardAction>(OnDrawCard);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<BeginTurnAction>(OnBeginTurn);
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
    
    protected abstract void BeginTurn();
}