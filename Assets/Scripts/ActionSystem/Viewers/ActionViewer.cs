using UnityEngine.Events;
using UnityEngine;

public abstract class ActionViewer : MonoBehaviour
{
    public abstract void ExecuteViewAction(GameAction action, UnityAction<ActionViewer> onCompleteCallback);

    public virtual bool WillViewAction(GameAction action)
    {
        return true;
    }
    
    protected void RegisterViewer<T>() where T : GameAction
    {
        ActionSystem.Instance.Viewers.AddViewer<T>(this);
    }

    protected void UnregisterViewer<T>() where T : GameAction
    {
        ActionSystem.Instance.Viewers.RemoveViewer<T>(this);
    }  
}
