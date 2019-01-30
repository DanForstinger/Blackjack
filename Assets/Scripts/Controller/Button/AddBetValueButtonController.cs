using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class AddBetValueButtonController : ButtonController
{
    [SerializeField] private Text valueText;
    
    private PlayerModel player;
    
    private Image image;

    private Button button;
    
    private int value = 100;
    
    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    
    public void Initialize(int value, Sprite sprite, PlayerModel player)
    {
        this.value = value;
        this.player = player;
        
        valueText.text = "" + value;
        image.sprite = sprite;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        ActionSystem.Instance.ListenerRegistry.AddActionListener<AdjustPlayerMoneyAction>(OnAdjustMoney);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        ActionSystem.Instance.ListenerRegistry.AddActionListener<AdjustPlayerMoneyAction>(OnAdjustMoney);
    }

    void Start()
    {
        ToggleButtonIfCanAfford();
    }

    void OnAdjustMoney(GameAction action)
    {
        ToggleButtonIfCanAfford();
    }

    void ToggleButtonIfCanAfford()
    {
        button.interactable = player.Money >= value;
    }
    
    protected override void OnButtonClicked()
    {
        if (player.Money >= value)
        {
            var action = new AddBetValueAction(value, image.sprite, player.PlayerIndex);
            ActionSystem.Instance.PerformAction(action);
        }
    }
}
