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
        "The stakes are high.",
        "BLACKJACK! Just kidding.",
        "Your turn bub."
    };

    private readonly string[] victoryLines =
    {
        "You counting cards?",
        "Nice! You did it!",
        "You win this one, bub."
    };

    private readonly string[] defeatLines =
    {
        "I don't mind taking your money.",
        "The house always wins.",
        "Today's not your day, pal."
    };
    
    void OnEnable()
    {  
        container.SetActive(false);
        
        ActionSystem.Instance.ListenerRegistry.AddActionListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<BeginTurnAction>(OnBeginTurn);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<DeclareGameResultAction>(OnDeclareGameResult);
    }

    void OnBeginTurn(GameAction action)
    {
        var beginTurnAction = (BeginTurnAction) action;

        //todo: Get a model here so that we can check if we have stayed
        if (beginTurnAction.OwningPlayer == 0)
        {
            StartCoroutine(ShowDialogue(startTurnLines[Random.Range(0, startTurnLines.Length)]));
        }
    }
    
    void OnDeclareGameResult(GameAction action)
    {
        var resultAction = (DeclareGameResultAction) action;

        //todo: Get a model here?
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
