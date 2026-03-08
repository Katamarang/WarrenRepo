using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card Card { get; private set; }
    Image image;
    Transform displayCard;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetCard(Card card, Transform display)
    {
        Card = card;
        displayCard = display;

        image.sprite = Card.CardSprite;
    }

    public void OnHover()
    {
        displayCard.GetComponent<Image>().sprite = Card.CardSprite;

        displayCard.GetChild(0).GetComponent<TMP_Text>().text = Card.CardName;
        displayCard.GetChild(1).GetComponent<TMP_Text>().text = Card.CardDescription;
    }
}
