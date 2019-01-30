using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreenController : MonoBehaviour
{
    [SerializeField] private GameObject BetPlacementUI;
    [SerializeField] private GameObject GameplayControlsUI;

    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.Listeners.AddListener<PlaceBetAction>(OnPlaceBet);

        BetPlacementUI.SetActive(true);
        GameplayControlsUI.SetActive(false);
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<StartGameAction>(OnStartGame);
        ActionSystem.Instance.Listeners.RemoveListener<PlaceBetAction>(OnPlaceBet);
    }

    void OnStartGame(GameAction action)
    {
        BetPlacementUI.SetActive(true);
        GameplayControlsUI.SetActive(false);
    }
    
    void OnPlaceBet(GameAction action)
    {
        BetPlacementUI.SetActive(false);
        GameplayControlsUI.SetActive(true);
    }
}
