using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel playerModel { get; private set; }

    public void Initialize(int index)
    {
        playerModel = new PlayerModel(index);
        
        Debug.Log(string.Format("Initialized a player controller at index {0}.", index));
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DrawCardAction>(OnDrawCard);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DrawCardAction>(OnDrawCard);
    }

    void OnDrawCard(GameAction action)
    {
        var drawCardAction = (DrawCardAction)action;

        if (drawCardAction.OwningPlayer == playerModel.PlayerIndex)
        {
            playerModel.AddCard(drawCardAction.Card);
        }
    }
}
