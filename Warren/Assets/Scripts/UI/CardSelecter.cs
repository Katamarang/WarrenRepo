using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelecter : MonoBehaviour
{
    // responsible for displaying and selecting cards.
    [SerializeField] CardDisplay[] cardSlots;
    [SerializeField] Transform cardDisplay;
    [SerializeField] WeaponCard Fists;

    [Space(20)]
    [SerializeField] int MaxCards;

    [Header("Error Handling")]
    [SerializeField] GameObject ExitScreen;

    List<Card> selectedCards = new List<Card>();
    bool meleeSelected;

    public event Action MeleeSelected;
    public event Action MeleeDeselected;


    private void Start()
    {
        CardManager.Instance.DisplayAllCards += OnDisplayAllCards;
    }

    private void OnDestroy()
    {
        CardManager.Instance.DisplayAllCards -= OnDisplayAllCards;
    }

    public void OnDisplayAllCards(List<Card> AllCards)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            
            cardSlots[i].SetCard(AllCards[i], cardDisplay, this);
        }
    }

    // returns false if the card cannot be added, true if it can. 
    public bool AddCard(Card card)
    {
        if (selectedCards.Count + 1 > MaxCards) { return false; }
        selectedCards.Add(card);

        // handles specific card types being added.
        if (card is WeaponCard) { meleeSelected = true; }

        return true;
    }

    // returns false if the card cannot be removed, true if it can.
    public bool RemoveCard(Card card)
    {
        if (selectedCards.Count - 1 < 0) { return false; }
        selectedCards.Remove(card);

        // handles specific card types being removed.
        if (card is WeaponCard) { meleeSelected = false; }

        return true;
    }

    #region Specific Card Selected
    public void OnMeleeSelected() { MeleeSelected?.Invoke(); }
    public void OnMeleeDeselected() { MeleeDeselected?.Invoke(); }
    #endregion

    public void CloseScreen()
    {
        ExitScreen.SetActive(true);
        TMP_Text text = ExitScreen.GetComponentInChildren<TMP_Text>();

        // handles error messages for not selecting a melee, spell, or enough cards.
        if (meleeSelected && selectedCards.Count == MaxCards) { text.text = "Confirm Cards?"; }
        else if (!meleeSelected) { text.text = "No melee Selected. Confirm Cards?"; }
        else if (selectedCards.Count != MaxCards) { text.text = "Less than five cards selected. Confirm Cards?"; }
    }

    public void AcceptChoices()
    {
        ExitScreen.SetActive(false);

        if (!meleeSelected) { selectedCards.Insert(0, Fists); }

        CardManager.Instance.SetActiveCards(selectedCards);
        GameManager.Instance.ChangeScene("PlayerScene");
        // save stuff and switch scenes
    }

    public void RefuseChoice()
    {
        ExitScreen.SetActive(false);
    }
}
