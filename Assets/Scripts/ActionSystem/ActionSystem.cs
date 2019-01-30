using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

[Serializable]
public class ActionEvent : UnityEvent<GameAction> {}

[CreateAssetMenu]
public class ActionSystem
{
    private static ActionSystem instance;

    public static ActionSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ActionSystem();
            }
            
            return instance; 
        }
    }

    public ActionEvent OnBeginPerform = new ActionEvent();
    public ActionEvent OnFinishPerform = new ActionEvent();
    
    public ActionViewerRegistry Viewers = new ActionViewerRegistry();
        
    public ActionListenerRegistry Listeners = new ActionListenerRegistry();

    private List<ActionViewer> currentlyExecutingViewers = new List<ActionViewer>();

    private GameAction currentlyExecutingAction;
    
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
            OnBeginPerform.Invoke(action);
            
            currentlyExecutingAction = action;
            
            if (Viewers.HasViewers(action))
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
        var potentialViewers = Viewers.GetViewers(action);
        
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
        Listeners.InvokePerformEvent(currentlyExecutingAction);

        if (actionQueue.Count > 0)
        {
            PerformAction(actionQueue.Dequeue());
        }
        else //we're done here
        {        
            OnFinishPerform.Invoke(currentlyExecutingAction);
        }
    }
}
