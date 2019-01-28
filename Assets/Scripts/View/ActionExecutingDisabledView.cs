using UnityEngine;
using UnityEngine.UI;

//This view disables its children while actions are executing.
public class ActionExecutingDisabledView : MonoBehaviour
{
    [SerializeField] private GameObject container;
    
    void OnEnable()
    {
        ActionSystem.Instance.OnBeginPerform.AddListener(OnBeginPerform);
        ActionSystem.Instance.OnFinishPerform.AddListener(OnFinishPerform);
    }

    void OnDisable()
    {
        ActionSystem.Instance.OnBeginPerform.RemoveListener(OnBeginPerform);
        ActionSystem.Instance.OnFinishPerform.RemoveListener(OnFinishPerform);
    }

    void OnBeginPerform(GameAction action)
    {
        Debug.Log("BEGIN");
        container.SetActive(false);
    }

    void OnFinishPerform(GameAction action)
    {
        Debug.Log("FINISH");
        container.SetActive(true);
    }
}
