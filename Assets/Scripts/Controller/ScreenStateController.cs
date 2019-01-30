using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(StackNavigator))]
public class ScreenStateController : MonoBehaviour
{
    [SerializeField] GameObject gameplayScreen;
    [SerializeField] GameObject gameOverScreen;

    private StackNavigator navigator;

    void Awake()
    {
        navigator = GetComponent<StackNavigator>();
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<StartGameAction>(OnGameStart);
        ActionSystem.Instance.Listeners.AddListener<EndGameAction>(OnGameEnd);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<StartGameAction>(OnGameStart);
        ActionSystem.Instance.Listeners.RemoveListener<EndGameAction>(OnGameEnd);
    }

    void OnGameStart(GameAction action)
    {
        navigator.Navigate(gameplayScreen);
    }

    void OnGameEnd(GameAction action)
    {
        navigator.Navigate(gameOverScreen);
    }
}
