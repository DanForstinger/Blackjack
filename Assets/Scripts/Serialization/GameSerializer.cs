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
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<EndGameAction>(OnEndGame);
    }

    void OnDisable()
    {
        SaveGameState();
       
        ActionSystem.Instance.Listeners.RemoveListener<EndGameAction>(OnEndGame);
    }

    void Start()
    {
        LoadGameState();
    }

    void OnEndGame(GameAction action)
    {
        SaveGameState();
    }
    
    void SaveGameState()
    {
        var gameStateJson = controller.Model.ToJson();
        
        //write it to player prefs.
        PlayerPrefs.SetString(saveGameKey, gameStateJson);
        
        Debug.Log(string.Format("Game State at End of Game:\n{0}", gameStateJson));
    }

    void LoadGameState()
    {
        if (PlayerPrefs.HasKey(saveGameKey))
        {
            var previousGameState = PlayerPrefs.GetString(saveGameKey);

            Debug.Log(string.Format("Previous Game End State:\n{0}", previousGameState));

            var model = JsonUtility.FromJson<GameModel>(previousGameState);

            //for now, just reload the players money.
            controller.LocalPlayer.SetMoney(model.Players[0].Money);
        }
    }
}
