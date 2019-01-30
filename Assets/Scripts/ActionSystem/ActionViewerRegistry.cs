using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionViewerRegistry
{
    private Dictionary<System.Type, List<ActionViewer>> actionViewerMap = new Dictionary<System.Type, List<ActionViewer>>();

    public bool HasViewers<T>(T action) where T : GameAction
    {
        var type = ActionSystem.GetActionType(action);

        return actionViewerMap.ContainsKey(type) && actionViewerMap[type].Count > 0;
    }
    
    public List<ActionViewer> GetViewers<T>(T action) where T : GameAction
    {
        var typeName = ActionSystem.GetActionType(action);

        if (actionViewerMap.ContainsKey(typeName))
        {
            return actionViewerMap[typeName];
        } 
        else
        {
            Debug.LogWarning(string.Format("No viewers present for {0}. Returning null.", typeName));
            return null;
        }
    }
    
    public void AddViewer<T>(ActionViewer viewer) where T : GameAction
    {
        var typeName = ActionSystem.GetActionType<T>();
        
        if (!actionViewerMap.ContainsKey(typeName))
        {
            actionViewerMap.Add(typeName, new List<ActionViewer>(1));
        }

        actionViewerMap[typeName].Add(viewer);
    }

    public void RemoveViewer<T>(ActionViewer viewer) where T : GameAction
    {
        var typeName = ActionSystem.GetActionType<T>();
        
        if (actionViewerMap.ContainsKey(typeName))
        {
            actionViewerMap[typeName].Remove(viewer);
        }
    }

}
