using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrentTurnTextView : MonoBehaviour
{
    private Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }
    
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
        
        var currentPlayer = turnAction.OwningPlayer == 0 ? "Player" : "Dealer";

        text.text = string.Format("{0}'s Turn!", currentPlayer);
    }
}
