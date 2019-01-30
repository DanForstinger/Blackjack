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
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DeclareWinnerAction>(OnWinnerDeclared);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DeclareWinnerAction>(OnWinnerDeclared);
    }

    void OnWinnerDeclared(GameAction action)
    {
        var winnerAction = (DeclareWinnerAction) action;

        bool didLocalPlayerWin = winnerAction.Winner.IsLocalPlayer;

        if (didLocalPlayerWin)
        {
            title.text = "You Win!";
        }
        else
        {
            title.text = "You Lost...";
        }

        var localPlayer = winnerAction.Winner.IsLocalPlayer ? winnerAction.Winner : winnerAction.Loser;
        var dealerPlayer = winnerAction.Winner.IsLocalPlayer ? winnerAction.Loser : winnerAction.Winner;

        localPlayerScore.text = "" + localPlayer.Score;
        dealerScore.text = "" + dealerPlayer.Score;
    }
}
