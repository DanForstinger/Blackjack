using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenController : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text localPlayerScore;
    [SerializeField] private Text dealerScore;
    [SerializeField] private GameObject confettiPrefab;
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnDeclareGameResult(GameAction action)
    {
        var resultAction = (DeclareGameResultAction) action;

        if (resultAction.Result == GameResult.PlayerWins)
        {
            title.text = "You Win!";
            Instantiate(confettiPrefab, Vector2.zero, Quaternion.identity);
        }
        else if (resultAction.Result == GameResult.Tie)
        {
            title.text = "Tie!";
        }
        else
        {
            title.text = "You Lost...";
        }

        localPlayerScore.text = "" + resultAction.LocalPlayer.Score;
        dealerScore.text = "" + resultAction.Dealer.Score;
    }
}
