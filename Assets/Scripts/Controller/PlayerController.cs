using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DrawCardAction>(OnDrawCard);
    }

    void OnDrawCard(GameAction action)
    {
        var drawCardAction = (DrawCardAction)action;

        if (drawCardAction.OwningPlayer == Model.PlayerIndex)
        {
            Model.AddCard(drawCardAction.Card);
        }
    }
}
