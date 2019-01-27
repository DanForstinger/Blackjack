using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField]private Image cardFront;
    [SerializeField]private Image cardBack;

    [SerializeField] private Sprite[] cardSprites;

    private CardModel card;

    public void SetCard(CardModel card, bool isRevealed)
    {
        this.card = card;

        var spriteIndex = (DeckModel.CardsPerSuit * (int) card.Suit) + card.Rank;

        if (cardSprites.Length > spriteIndex)
        {
            cardFront.sprite = cardSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(string.Format("Not enough sprites to populate card with rank {0} and suit {1}", card.Rank, card.Suit));
        }
        
        SetRevealed(isRevealed);
    }

    public void SetRevealed(bool isRevealed)
    {
        cardFront.enabled = isRevealed;
        cardBack.enabled = !isRevealed;
    }
}
