using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrentBetView : MonoBehaviour
{
    private Text text;

    private int bet = 0;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<AddBetValueAction>(OnAddValue); 
        ActionSystem.Instance.ListenerRegistry.AddActionListener<PlaceBetAction>(OnPlaceBet); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<AddBetValueAction>(OnAddValue);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<PlaceBetAction>(OnPlaceBet); 
    }


    void OnAddValue(GameAction action)
    {
        var betAction = (AddBetValueAction) action;

        bet += betAction.Value;
        
        text.text = string.Format("${0}", bet);
    }
    
    //todo: Place bet should be the only action, and value should accumulate in the controller?
    void OnPlaceBet(GameAction action)
    {
        var betAction = (PlaceBetAction) action;

        bet = betAction.Value;
        
        text.text = string.Format("${0}", bet);
    }
}
