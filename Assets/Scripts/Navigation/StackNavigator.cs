using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StackNavigator : MonoBehaviour
{
    [SerializeField] private int firstScreenIndex = 0;
    [SerializeField] private GameObject[] screens;

    private Stack<GameObject> navigationStack = new Stack<GameObject>();
    
    void Start()
    {
        HideAll();
        
        Navigate(screens[firstScreenIndex]);
    }
    
    public void Navigate(GameObject screen)
    {
        if (navigationStack.Count > 0)
        {
            navigationStack.Peek().SetActive(false);
        }

        screen.SetActive(true);
        navigationStack.Push(screen);
    }

    public void NavigateBack()
    {
        if (navigationStack.Count > 1)
        {
            var toHide = navigationStack.Pop();
            toHide.SetActive(false);
            
            navigationStack.Peek().SetActive(true);
        }
    }

    void HideAll()
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
    }
}