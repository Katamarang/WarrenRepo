using System;
using UnityEngine;
using System.Collections.Generic;

public class CardManager : Singleton<CardManager>
{
    public List<Card> AllCards;

    public List<Card> ActiveCards;

    public event Action<List<Card>> DisplayAllCards;

    public event Action<List<Card>> SendPlayerCards;
    public event Action<List<Card>> SendWorldCards;

    [ContextMenu("Load Cards")]
    public void LoadCards()
    {
        List<Card> playerCards = new List<Card>();
        //List<Card> worldCards = new List<Card>();

        foreach (Card card in ActiveCards)
        {
            if (card is PlayerCard) { playerCards.Add(card); }
            //else if (card is WorldCard) { worldCards.Add(card); }
        }

        SendPlayerCards.Invoke(playerCards);
        //SendWorldCards.Invoke(worldCards);
    }

    [ContextMenu("Display Cards")]
    public void DisplayCards()
    {
        DisplayAllCards.Invoke(AllCards);
    }
}
