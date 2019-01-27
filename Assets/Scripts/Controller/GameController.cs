using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController[] players;

    private DeckModel deck;

    void Awake()
    {
        deck = new DeckModel();

        for (int i = 0; i < players.Length; ++i)
        {
            players[i].Initialize(i);
        }
    }
    
    void Start()
    {
        ActionSystem.Instance.PerformAction(new DrawCardAction(deck.DrawCard(), 0, true));
        ActionSystem.Instance.PerformAction(new DrawCardAction(deck.DrawCard(), 0, true));
        ActionSystem.Instance.PerformAction(new DrawCardAction(deck.DrawCard(), 1, false));
        ActionSystem.Instance.PerformAction(new DrawCardAction(deck.DrawCard(), 1, true));
    }
}
