using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats, IStats
{
    // subclass of EntityStats, holds all stats unique to the Player. Also handles card loading.

    [Header("Parry")]
    public float ParryWindow = 0.6f;

    [Header("Spell")]
    public int SpellCost;
    public int SpellDamage;
    public float SpellRadius;
    public float SpellLength;
    public List<StatusEffect> SpellDamageTypes = new List<StatusEffect>();
    public WeaponBehaviour SpellBehaviour;  

    #region Initialization
    private void OnEnable()
    {
        CardManager.Instance.SendPlayerCards += OnPlayerCardsRecieved;       
    }

    private void OnDisable()
    {
        CardManager.Instance.SendPlayerCards -= OnPlayerCardsRecieved;
    }
    #endregion

    private void OnPlayerCardsRecieved(List<Card> cards)
    {
        this.cards = cards;
        print("Cards Recieved");
        LoadCardLoader();
        GetComponent<EntityHealth>().Load();
    }

    public void LoadCardLoader()
    {
        cardLoader = new CardLoader(this);
        cardLoader.LoadPlayerCards(cards);
    }


}
