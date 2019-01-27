using UnityEngine.Events;
using UnityEngine;

public abstract class ActionViewer : MonoBehaviour
{
    public abstract void ExecuteViewAction(GameAction action, UnityAction<ActionViewer> onCompleteCallback);

    protected void RegisterViewer<T>() where T : GameAction
    {
        ActionSystem.Instance.ViewerRegistry.UnregisterViewer<T>(this);
    }

    protected void UnregisterViewer<T>() where T : GameAction
    {
        ActionSystem.Instance.ViewerRegistry.RegisterViewer<T>(this);
    }
}
