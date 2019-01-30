using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerDialogueController : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Text dialogueText;

    private const float dialogueStartTime = 0.2f;
    private const float dialoguePrintSpeed = 0.01f;
    private const float dialogueDisplayTime = 2.0f;
    
    //This should be its own file.
    private readonly string[] startTurnLines = 
    {
        "What're you gonna do?",
        "I believe in you.",
        "I'd hit that",
        "Don't take all day.",
        "Think you've got what it takes?",
        "You can do it, bub.",
        "The stakes are high.",
        "BLACKJACK! ... Just kidding",
        "Your turn, bub."
    };

    private readonly string[] victoryLines =
    {
        "You counting cards?",
        "Nice! You did it!",
        "Don't spend it all in one place.",
        "Don't forget to tip your dealer.",
        "You win this one, bub."
    };

    private readonly string[] defeatLines =
    {
        "I don't mind taking your money.",
        "The house always wins.",
        "There's always next time.",
        "Today's not your day, pal."
    };
    
    void OnEnable()
    {  
        container.SetActive(false);
        
        ActionSystem.Instance.Listeners.AddListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.Listeners.AddListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.Listeners.RemoveListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnBeginTurn(GameAction action)
    {
        var beginTurnAction = (BeginTurnAction) action;

        var player = beginTurnAction.Player;

        if (player.IsLocalPlayer && !player.DidStay && !player.DidBust)
        {
            StartCoroutine(ShowDialogue(startTurnLines[Random.Range(0, startTurnLines.Length)]));
        }
    }
    
    void OnDeclareGameResult(GameAction action)
    {
        var resultAction = (DeclareGameResultAction) action;

        if (resultAction.Result == GameResult.PlayerWins)
        {
            StartCoroutine(ShowDialogue(victoryLines[Random.Range(0, victoryLines.Length)]));
        }
        else
        {
            StartCoroutine(ShowDialogue(defeatLines[Random.Range(0, defeatLines.Length)]));
        }
    }


    IEnumerator ShowDialogue(string text)
    {
        var wait = new WaitForSeconds(dialogueStartTime);
        yield return wait;
          
        container.SetActive(true);

        wait = new WaitForSeconds(dialoguePrintSpeed);
        
        string dialogue = "";
        for (int i = 0; i < text.Length; ++i)
        {
            dialogue += text[i];
            dialogueText.text = dialogue;
            yield return wait;
        }

        wait = new WaitForSeconds(dialogueDisplayTime);
        yield return wait;

        container.SetActive(false);
    }
}
