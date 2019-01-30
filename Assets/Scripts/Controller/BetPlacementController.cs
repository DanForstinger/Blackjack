using UnityEngine;

public class BetPlacementController : MonoBehaviour
{
    [System.Serializable]
    class BetValueModel
    {
        public int Value;
        public Sprite ChipSprite;
    }
    
    public int CurrentBet { get; private set; }

    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private AddBetValueButtonController chipPrefab;
 
    [SerializeField] private Transform chipContainer;

    [SerializeField] private BetValueModel[] chipValues;

    void Start()
    {
        foreach (var chip in chipValues)
        {
            CreateChip(chip);
        }

        CurrentBet = 0;
    }
    
    private void CreateChip(BetValueModel chip)
    {
        var chipObj = Instantiate(chipPrefab.gameObject) as GameObject;
        var chipButton = chipObj.GetComponent<AddBetValueButtonController>();
        
        chipButton.Initialize(chip.Value, chip.ChipSprite, playerController.Model);
        
        chipObj.transform.SetParent(chipContainer);
        chipObj.transform.localScale = Vector3.one;
    }

    void OnEnable()
    {
        ActionSystem.Instance.ListenerRegistry.AddActionListener<AddBetValueAction>(OnAddBetValue);
    }

    void OnDisable()
    {
        ActionSystem.Instance.ListenerRegistry.RemoveActionListener<AddBetValueAction>(OnAddBetValue);
    }

    void OnAddBetValue(GameAction action)
    {
        var betAction = (AddBetValueAction) action;
        CurrentBet += betAction.Value;
    }

}
