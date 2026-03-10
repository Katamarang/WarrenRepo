using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCards : MonoBehaviour
{
    [SerializeField] CardDisplay[] cards;
    [SerializeField] Transform cardDisplay;

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
        for (int i = 0; i < cards.Length; i++)
        {
            CardDisplay card = cards[i];
            card.SetCard(AllCards[i], cardDisplay, this);
        }
    }

    public void OnMeleeSelected()
    {
        MeleeSelected.Invoke();
    }

    public void OnMeleeDeselected()
    {
        MeleeDeselected.Invoke();
    }
}
