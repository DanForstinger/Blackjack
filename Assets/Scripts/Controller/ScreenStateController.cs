using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenStateController : MonoBehaviour
{
    //todo: The more screens you add the worse this gets.
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameplay;
    [SerializeField] GameObject gameOver;
    
    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<StartGameAction>(OnGameStart);
        ActionSystem.Instance.ListenerRegistry.AddActionListener<EndGameAction>(OnGameEnd);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<StartGameAction>(OnGameStart);
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<EndGameAction>(OnGameEnd);
    }

    void Start()
    {
        mainMenu.SetActive(true);
        
        gameplay.SetActive(false);
        gameOver.SetActive(false);
    }
    
    void OnGameStart(GameAction action)
    {
        gameplay.SetActive(true);

        mainMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    void OnGameEnd(GameAction action)
    {
        gameOver.SetActive(true);
        
        mainMenu.SetActive(false);
        gameplay.SetActive(false);
    }
}
