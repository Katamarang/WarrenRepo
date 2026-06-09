using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGCardHolder : MonoBehaviour
{
    // debug script for displaying the cards in DEBUGTraitChoice's dropdown.
    
    Card Card;
    DEBUGTraitChoice Parent;

    public TMP_Text Text;
    public Button Button;
    public bool selected;

    public void DisplayCard(Card card, DEBUGTraitChoice parent)
    {
        Card = card;
        Parent = parent;
        Text.text = card.CardName;
    }

    public void OnClick()
    {
        selected = !selected;

        if (selected)
        {
            Button.image.color = Color.gray;
            Parent.AddCard(Card);
        } else
        {
            Button.image.color = Color.white;
            Parent.RemoveCard(Card);
        }
    }

}
