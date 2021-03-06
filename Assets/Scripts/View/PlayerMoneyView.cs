﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }
    
    void OnEnable()
    {
        ActionSystem.Instance.Listeners.AddListener<AdjustPlayerMoneyAction>(OnAdjustMoney); 
    }

    void OnDisable()
    {
        ActionSystem.Instance.Listeners.RemoveListener<AdjustPlayerMoneyAction>(OnAdjustMoney);
    }

    void Start()
    {
        SetValue(playerController.Model.Money);
    }

    void OnAdjustMoney(GameAction action)
    {
        var adjustAction = (AdjustPlayerMoneyAction) action;

        SetValue(adjustAction.NewValue);
    }

    void SetValue(int value)
    {
        text.text = string.Format("${0}", value);
    }
}
