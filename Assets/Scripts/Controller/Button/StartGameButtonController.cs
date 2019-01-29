using UnityEngine;

public class StartGameButtonController : ButtonController
{
    protected override void OnButtonClicked()
    {
        var startGameAction = new StartGameAction();
        ActionSystem.Instance.PerformAction(startGameAction);
    }
}
