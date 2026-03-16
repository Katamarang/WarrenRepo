using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] CardDisplay[] cardSlots;
    [SerializeField] Transform cardDisplay;

    [Space(20)]
    [SerializeField] int MaxCards;

    [Header("Error Handling")]
    [SerializeField] GameObject ExitScreen;

    List<Card> selectedCards = new List<Card>();
    bool meleeSelected;
    bool spellSelected;

    public event Action MeleeSelected;
    public event Action MeleeDeselected;

    public event Action SpellSelected;
    public event Action SpellDeselected;

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
            CardDisplay card = cardSlots[i];
            card.SetCard(AllCards[i], cardDisplay, this);
        }
    }

    public bool AddCard(Card card)
    {
        if (selectedCards.Count + 1 > MaxCards) { return false; }
        selectedCards.Add(card);

        if (card is WeaponCard) { meleeSelected = true; }
        else if (card is SpellCard) { spellSelected = true; }

        return true;
    }

    public bool RemoveCard(Card card)
    {
        if (selectedCards.Count - 1 < 0) { return false; }
        selectedCards.Remove(card);

        if (card is WeaponCard) { meleeSelected = false; }
        else if (card is SpellCard) { spellSelected = false; }

        return true;
    }

    #region Specific Card Selected
    public void OnMeleeSelected() { MeleeSelected?.Invoke(); }
    public void OnMeleeDeselected() { MeleeDeselected?.Invoke(); }
    public void OnSpellSelected() {  SpellSelected?.Invoke(); }
    public void OnSpellDeselected() { SpellDeselected?.Invoke(); }
    #endregion

    public void CloseScreen()
    {
        ExitScreen.SetActive(true);
        TMP_Text text = ExitScreen.GetComponentInChildren<TMP_Text>();

        if (meleeSelected && selectedCards.Count == MaxCards) { text.text = "Confirm Cards?"; }
        else if (!meleeSelected) { text.text = "No melee Selected. Confirm Cards?"; }
        else if (selectedCards.Count != MaxCards) { text.text = "Less than five cards selected. Confirm Cards?"; }
    }

    public void AcceptChoices()
    {
        ExitScreen.SetActive(false);
        CardManager.Instance.SetActiveCards(selectedCards);
        GameManager.Instance.ChangeScene("SampleScene");
        // save stuff and switch scenes
    }

    public void RefuseChoice()
    {
        ExitScreen.SetActive(false);
    }
}
