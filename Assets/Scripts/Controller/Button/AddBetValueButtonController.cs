using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class AddBetValueButtonController : ButtonController
{
    [SerializeField] private Text valueText;
    
    private int playerIndex;
    
    private Image image;

    private int value = 100;
    
    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void Initialize(int value, Sprite sprite, int playerIndex)
    {
        this.value = value;
        this.playerIndex = playerIndex;
        
        valueText.text = "" + value;
        image.sprite = sprite;
    }

    protected override void OnButtonClicked()
    {
        var action = new AddBetValueAction(value, image.sprite, playerIndex);
        ActionSystem.Instance.PerformAction(action);
    }
}
