using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class ActionListenerRegistry
{
    private Dictionary<Type, ActionEvent> listeners = new Dictionary<Type, ActionEvent>();

    
    public void AddListener<T>(UnityAction<GameAction> listener) where T: GameAction
    {
        var typeName = ActionSystem.GetActionType<T>();
        
        if (!listeners.ContainsKey(typeName))
        {
            listeners.Add(typeName, new ActionEvent());
        }

        listeners[typeName].AddListener(listener);
    }

    public void RemoveListener<T>(UnityAction<GameAction> listener) where T: GameAction
    {
        var typeName = ActionSystem.GetActionType<T>();
        
        if (listeners.ContainsKey(typeName))
        {
            listeners[typeName].RemoveListener(listener);
        }
    }

    public void InvokePerformEvent<T>(T action) where T : GameAction
    {
        var typeName = ActionSystem.GetActionType(action);

        if (listeners.ContainsKey(typeName))
        {
            var performEvent = listeners[typeName];
            performEvent.Invoke(action);
        }
    }
}
