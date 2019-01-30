using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class IntSliderController : MonoBehaviour
{
    [SerializeField] private Text valueText;
    [SerializeField] private Text titleText;

    private IntRangeValue intValue;

    private Slider slider;
    
    public void Initialize(IntRangeValue value)
    {
        intValue = value;
        titleText.text = value.Title;

        var sliderValue = GetSliderValue();
        slider.value = sliderValue;

        OnValueChanged(sliderValue);
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float newValue)
    {
        intValue.Value = SliderToValue(newValue);

        valueText.text = "" + intValue.Value;
    }

    float GetSliderValue()
    {
         return (float)(intValue.Value - intValue.MinValue) / (float)(intValue.MaxValue - intValue.MinValue);
    }

    int SliderToValue(float value)
    {
        return intValue.MinValue + (int)((intValue.MaxValue - intValue.MinValue) * value);
    }
}
