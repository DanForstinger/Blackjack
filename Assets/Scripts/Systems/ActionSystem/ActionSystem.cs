using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class ActionSystem : MonoBehaviour
{
    //todo: remove singelton
    private static ActionSystem instance;

    public static ActionSystem Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("ActionSystem");
                instance = obj.AddComponent<ActionSystem>();
            }
            
            return instance; 
        }
    }

    public ActionViewerRegistry ViewerRegistry = new ActionViewerRegistry();
        
    public ActionListenerRegistry ListenerRegistry = new ActionListenerRegistry();

    private List<ActionViewer> currentlyExecutingViewers = new List<ActionViewer>();

    private Queue<GameAction> actionQueue = new Queue<GameAction>();
        
    //Used when we only have the type
    public static Type GetActionType<T>() where T : GameAction
    {
        return typeof(T);
    }

    //Used when we have an action object. 
    public static Type GetActionType(GameAction action)
    {
        return action.GetType();
    }

    public bool IsExecutingAction()
    {
        return currentlyExecutingViewers.Count > 0;
    }
    
    public void PerformAction<T>(T action) where T : GameAction
    {
        if (IsExecutingAction())
        {
            actionQueue.Enqueue(action);
        }
        else
        {
            ListenerRegistry.InvokePerformEvent(action);

            if (ViewerRegistry.HasViewers(action))
            {
                StartActionViewers(action);
            }
            else
            {
                CompleteAction();
            }
        }
    }

    private void StartActionViewers<T>(T action) where T : GameAction
    {
        var potentialViewers = ViewerRegistry.GetViewers(action);
        
        // filter viewers based on if they want to view this action.
        foreach (var viewer in potentialViewers)
        {
            if (viewer.WillViewAction(action))
            {
                currentlyExecutingViewers.Add(viewer);
            }
        }

        // start viewing actions
        foreach (var viewer in currentlyExecutingViewers)
        {
            viewer.ExecuteViewAction(action, OnViewerComplete);
        }
    }

    private void OnViewerComplete(ActionViewer viewer)
    {
        currentlyExecutingViewers.Remove(viewer);
        
        if (!IsExecutingAction())
        {
            CompleteAction();
        }
    }

    private void CompleteAction()
    {
        if (actionQueue.Count > 0)
        {
            PerformAction(actionQueue.Dequeue());
        }
    }
}
