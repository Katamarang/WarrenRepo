using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCards : MonoBehaviour
{
    [SerializeField] CardDisplay[] cardSlots;
    [SerializeField] Transform cardDisplay;

    [Space(20)]
    [SerializeField] int MaxCards;

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
            CardDisplay card = cardSlots[i];
            card.SetCard(AllCards[i], cardDisplay, this);
        }
    }

    public bool AddCard(Card card)
    {
        if (selectedCards.Count + 1 > MaxCards) { return false; }
        selectedCards.Add(card);

        if (card is WeaponCard && (card as WeaponCard).WeaponType == WeaponType.Attack) { meleeSelected = true; }

        return true;
    }

    public bool RemoveCard(Card card)
    {
        if (selectedCards.Count - 1 < 0) { return false; }
        selectedCards.Remove(card);

        if (card is WeaponCard && (card as WeaponCard).WeaponType == WeaponType.Attack) { meleeSelected = false; }

        return true;
    }

    #region Specific Card Selected

    public void OnMeleeSelected()
    {
        MeleeSelected.Invoke();
    }

    public void OnMeleeDeselected()
    {
        MeleeDeselected.Invoke();
    }
    #endregion
}
