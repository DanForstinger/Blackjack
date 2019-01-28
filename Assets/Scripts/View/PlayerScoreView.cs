using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text scoreText;
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<UpdateScoreAction>(OnUpdateScore); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<UpdateScoreAction>(OnUpdateScore); 
    }

    public void OnUpdateScore(GameAction action)
    {
        var updateScoreAction = (UpdateScoreAction) action;
        
        scoreText.text = "" + updateScoreAction.Score;
    }
}
