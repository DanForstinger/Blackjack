using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(StackNavigator))]
public class ScreenStateController : MonoBehaviour
{
    //todo: should this dependency be shared with StackNavigator
    [SerializeField] GameObject gameplayScreen;
    [FormerlySerializedAs("gameOver")] [SerializeField] GameObject gameOverScreen;

    private StackNavigator navigator;

    void Awake()
    {
        navigator = GetComponent<StackNavigator>();
    }
    
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

    void OnGameStart(GameAction action)
    {
        navigator.Navigate(gameplayScreen);
    }

    void OnGameEnd(GameAction action)
    {
        navigator.Navigate(gameOverScreen);
    }
}
