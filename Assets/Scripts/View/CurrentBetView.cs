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
        ActionSystem.Instance.Listeners.AddListener<AddBetValueAction>(OnAddValue); 
        ActionSystem.Instance.Listeners.AddListener<PlaceBetAction>(OnPlaceBet); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<AddBetValueAction>(OnAddValue);
        ActionSystem.Instance.Listeners.RemoveListener<PlaceBetAction>(OnPlaceBet); 
    }


    void OnAddValue(GameAction action)
    {
        var betAction = (AddBetValueAction) action;

        bet += betAction.Value;
        
        text.text = string.Format("${0}", bet);
    }
    
    void OnPlaceBet(GameAction action)
    {
        var betAction = (PlaceBetAction) action;

        bet = betAction.Value;
        
        text.text = string.Format("${0}", bet);
    }
}
