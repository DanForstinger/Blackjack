using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class ActionEvent : UnityEvent<GameAction>
{
}

[CreateAssetMenu]
public class ActionSystem : ScriptableObject
{
    public Dictionary<string, ActionEvent> performEventListeners = new Dictionary<string, ActionEvent>();

    public void AddActionListener<T>(UnityAction<GameAction> listener) where T: GameAction
    {
        var typeName = GetNameForActionType<T>();
        
        if (!performEventListeners.ContainsKey(typeName))
        {
            performEventListeners.Add(typeName, new ActionEvent());
        }

        performEventListeners[typeName].AddListener(listener);
    }

    public void RemoveActionListener<T>(UnityAction<GameAction> listener) where T: GameAction
    {
        var typeName = GetNameForActionType<T>();
        
        if (performEventListeners.ContainsKey(typeName))
        {
            performEventListeners[typeName].RemoveListener(listener);
        }
    }
    
    public void PerformAction<T>(T action) where T : GameAction
    {
        var typeName = GetNameForActionType<T>();

        if (performEventListeners.ContainsKey(typeName))
        {
            var performEvent = performEventListeners[typeName];
            performEvent.Invoke(action);
        }
    }
    
    private string GetNameForActionType<T>() where T : GameAction
    {
        return typeof(T).Name;
    }
}
