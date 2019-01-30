using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateBackButtonController : ButtonController
{
    [SerializeField] private StackNavigator navigator;

    protected override void OnButtonClicked()
    {
        navigator.NavigateBack();
    }
}
