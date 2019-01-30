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
        ActionSystem.Instance.Listeners.AddListener<BeginTurnAction>(OnBeginTurn); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<BeginTurnAction>(OnBeginTurn); 
    }


    void OnBeginTurn(GameAction action)
    {
        var turnAction = (BeginTurnAction) action;
        
        var currentPlayer = turnAction.Player.IsLocalPlayer ? "Player" : "Dealer";

        text.text = string.Format("{0}'s Turn!", currentPlayer);
    }
}
