﻿using UnityEngine;

public class HitButtonController : ButtonController
{
    [SerializeField] private PlayerController playerController;
    
    protected override void OnButtonClicked()
    {
        var hitAction = new HitAction(playerController.Model);
        ActionSystem.Instance.PerformAction(hitAction);
    }
}
