using UnityEngine;
using UnityEngine.UI;

/*
 * This class provides a simple implementation of "OnClick" event so that children can fire behaviour on press.
 */
[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    protected virtual void OnDisable()
    {        
        var button = GetComponent<Button>();
        button.onClick.RemoveListener(OnButtonClicked);
    }

    protected abstract void OnButtonClicked();
}
