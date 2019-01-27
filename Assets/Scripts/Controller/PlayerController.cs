using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ActionSystem actionSystem;

    private PlayerModel playerModel;

    public void Initialize(int index)
    {
        playerModel = new PlayerModel(index);    
    }
    
    void OnEnable()
    {
        actionSystem.AddActionListener<DrawCardAction>(OnDrawCard);
    }

    void OnDisable()
    {
        actionSystem.RemoveActionListener<DrawCardAction>(OnDrawCard);
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
