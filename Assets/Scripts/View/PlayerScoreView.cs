using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text scoreText;
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<UpdateScoreAction>(OnUpdateScore); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<UpdateScoreAction>(OnUpdateScore); 
    }

    public void OnUpdateScore(GameAction action)
    {
        var updateScoreAction = (UpdateScoreAction) action;

        if (updateScoreAction.Player.PlayerIndex == playerController.Model.PlayerIndex)
        {
            scoreText.text = "" + updateScoreAction.Score;
        }
    }
}
