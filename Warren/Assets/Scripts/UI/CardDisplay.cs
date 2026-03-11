using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card Card { get; private set; }
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
        StopListening();
    }

    public void SetCard(Card card, Transform hoverDisplay, CardSelecter display)
    {
        Card = card;
        hoveredCardDisplay = hoverDisplay;
        this.display = display;

        StartListening();

        cardSelectSprite = transform.GetChild(0).gameObject;
        cardSelectSprite.SetActive(false);

        image.sprite = Card.CardSprite;
    }

    public void OnHover()
    {
        hoveredCardDisplay.GetComponent<Image>().sprite = Card.CardSprite;

        hoveredCardDisplay.GetChild(0).GetComponent<TMP_Text>().text = Card.CardName;
        hoveredCardDisplay.GetChild(1).GetComponent<TMP_Text>().text = Card.CardDescription;
    }

    public void OnClick() 
    {
        if (buttonPressed && !display.RemoveCard(Card)) { return; }
        if (!buttonPressed && !display.AddCard(Card)) { return; }

        buttonPressed = !buttonPressed;
        cardSelectSprite.SetActive(buttonPressed);

        if (Card is WeaponCard && (Card as WeaponCard).WeaponType == WeaponType.Attack)
        {
            if (buttonPressed)
            {
                display.OnMeleeSelected();
            }
            else
            {
                display.OnMeleeDeselected();
            }
        }
    }

    private void OnMeleeSelected()
    {
        //print("melee selected");

        if (!buttonPressed)
        {
            button.interactable = false;
            image.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void OnMeleeDeselected()
    {
        //print("melee deselected");

        button.interactable = true;
        image.color = new Color(1, 1, 1, 1);
    }

    #region listeners

    private void StartListening()
    {
        if (Card is not WeaponCard) { return; } // only listens if card is a weapon

        if ((Card as WeaponCard).WeaponType == WeaponType.Attack)
        {
            display.MeleeSelected += OnMeleeSelected;
            display.MeleeDeselected += OnMeleeDeselected;
        }
    }

    private void StopListening()
    {
        display.MeleeSelected -= OnMeleeSelected;
        display.MeleeDeselected -= OnMeleeDeselected;
    }
    #endregion
}
