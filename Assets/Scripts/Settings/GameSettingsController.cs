using UnityEngine;
using System;

public class GameSettingsController : MonoBehaviour
{
    [SerializeField] private IntRangeValue[] intSettings;
    [SerializeField] private IntSliderController sliderPrefab;
    [SerializeField] private Transform sliderContainer;
    
    void Awake()
    {
        foreach (var value in intSettings)
        {
            var slider = Instantiate(sliderPrefab.gameObject, sliderContainer) as GameObject;
            var sliderController = slider.GetComponent<IntSliderController>();

            sliderController.Initialize(value);
        }
    }
}
