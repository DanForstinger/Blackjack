using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameSerializer : MonoBehaviour
{
     private GameController controller;
    
    private const string saveGameKey = "SaveGame";

    void Awake()
    {
        controller = GetComponent<GameController>();

        var previousGameState = PlayerPrefs.GetString(saveGameKey);
        
        Debug.Log(string.Format("Previous Game End State:\n{0}", previousGameState));
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<EndGameAction>(OnEndGame);
    }

    void OnDisable()
    {
        //todo: Rename to event system?
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<EndGameAction>(OnEndGame);
    }

    void OnEndGame(GameAction action)
    {
        SaveGameState();
    }
    
    //todo: new class?
    void SaveGameState()
    {
        var gameStateJson = controller.Model.ToJson();
        
        //write it to player prefs.
        PlayerPrefs.SetString(saveGameKey, gameStateJson);
        
        Debug.Log(string.Format("Game State at End of Game:\n{0}", gameStateJson));
    }
}
