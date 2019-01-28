using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerScoreViewer : PlayerActionViewer
{
    [SerializeField] private Text scoreText;
    
    void OnEnable()
    {
        RegisterViewer<UpdateScoreAction>();
    }

    void OnDisable()
    {
        UnregisterViewer<UpdateScoreAction>();
    }

    public override void ExecuteViewAction(GameAction action, UnityAction<ActionViewer> onCompleteCallback)
    {
        var updateScoreAction = (UpdateScoreAction) action;
        
        scoreText.text = "" + updateScoreAction.Score;
        
        onCompleteCallback.Invoke(this);
    }
}
