using System;
using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public List<Card> AllCards;

    List<Card> ActiveCards;

    public StatusEffect[] AllStatusEffects;

    public event Action<List<Card>> DisplayAllCards;

    public event Action<List<Card>> SendPlayerCards;
    public event Action<List<Card>> SendWorldCards;

    public static CardManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DisplayAllCards?.Invoke(AllCards);
    }

    public void LoadCards()
    {
        List<Card> playerCards = new List<Card>();
        //List<Card> worldCards = new List<Card>();

        foreach (Card card in ActiveCards)
        {
            if (card is PlayerCard) { playerCards.Add(card); }
            //else if (card is WorldCard) { worldCards.Add(card); }
        }

        SendPlayerCards?.Invoke(playerCards);
        
        //SendWorldCards.Invoke(worldCards);
    }

    public void SetActiveCards(List<Card> activeCards)
    {
        ActiveCards = activeCards;
    }

    #region Save Load
    public void Save(ref StartingDeckData data)
    {
        data.StarterDeck = ActiveCards;
    }
    public void Load(ref StartingDeckData data)
    {
        ActiveCards = data.StarterDeck;
        LoadCards();
    }
    #endregion

}

[System.Serializable]
public struct StartingDeckData
{
    public List<Card> StarterDeck;
}
