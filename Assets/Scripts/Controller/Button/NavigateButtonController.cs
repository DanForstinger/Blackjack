using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateButtonController : ButtonController
{
    [SerializeField] private StackNavigator navigator;

    public GameObject screen;
    
    protected override void OnButtonClicked()
    {
        navigator.Navigate(screen);
    }
}
