using UnityEngine;
using UnityEngine.UI;

public class ActionExecutingDisabledView : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private GameObject toggledObject;

    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<BeginTurnAction>(OnBeginTurn); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<BeginTurnAction>(OnBeginTurn); 
    }

    void OnBeginTurn(GameAction action)
    {
        var turnAction = (BeginTurnAction) action;
        toggledObject.SetActive(turnAction.OwningPlayer == playerController.Model.PlayerIndex);
    }
}
