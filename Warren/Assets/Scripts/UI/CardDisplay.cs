using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // responsible for displaying a card in the card select menu, and handling the selection of that card.
    public Spell Card { get; private set; }
    Image image;
    Button button;
    Transform hoveredCardDisplay;
    GameObject cardSelectSprite;
    CardSelecter display;


    [SerializeField] bool buttonPressed;
    

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void OnDisable()
    {
        //StopListening();
    }

    public void SetCard(Spell card, Transform hoverDisplay, CardSelecter display)
    {
        if (card == null) { gameObject.SetActive(false); return; }

        Card = card;
        hoveredCardDisplay = hoverDisplay;
        this.display = display;

        //StartListening();

        cardSelectSprite = transform.GetChild(0).gameObject;
        cardSelectSprite.SetActive(false);

        image.sprite = Card.IconSprite;
    }

    public void OnHover()
    {
        hoveredCardDisplay.GetComponent<Image>().sprite = Card.IconSprite;

        hoveredCardDisplay.GetChild(0).GetComponent<TMP_Text>().text = Card.Name;
        hoveredCardDisplay.GetChild(1).GetComponent<TMP_Text>().text = Card.Desc;
    }

    public void OnClick() 
    {
        //if (buttonPressed && !display.RemoveCard(Card)) { return; }
        //if (!buttonPressed && !display.AddCard(Card)) { return; }

        buttonPressed = !buttonPressed;
        cardSelectSprite.SetActive(buttonPressed);

        /*if (buttonPressed) // used if specific cards have been selected.
        {
            if (Card is WeaponCard) { display.OnMeleeSelected(); }
        }
        else
        {
            if (Card is WeaponCard) { display.OnMeleeDeselected(); }
        }*/

    }

    private void OnCardSelected()
    {
        if (!buttonPressed)
        {
            button.interactable = false;
            image.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void OnCardDeselected()
    {
        button.interactable = true;
        image.color = new Color(1, 1, 1, 1);
    }

    #region listeners

    /*private void StartListening()
    {
        //if (Card is not WeaponCard or SpellCard) { return; } // only listens if card is a weapon or spell

        if (Card is WeaponCard)
        {
            display.MeleeSelected += OnCardSelected;
            display.MeleeDeselected += OnCardDeselected;
        }
    }

    private void StopListening()
    {
        if (display == null) { return; }
        display.MeleeSelected -= OnCardSelected;
        display.MeleeDeselected -= OnCardDeselected;
    }*/
    #endregion
}
