using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DEBUGTraitChoice : MonoBehaviour
{
    // A debug script for adding and removing cards without needing to change scenes.
    
    [SerializeField] GameObject button;

    [SerializeField] List<Card> selectedCards;

    [SerializeField] Entity Player;
    PlayerStats playerStats;
    PlayerCombat playerCombat;
    EntityHealth entityHealth;
    

    // Stores the player's stats so that they can be reset when applying or resetting cards
    #region Stored Stats
    int MaxHealth;
    int CurrentHealth;

    float SpeedModifier;
    float Acceleration;

    float MeleeCooldown = 0.3f;
    int PrimaryDamage;
    float PrimaryRadius;

    int SpellCost;
    int SpellDamage;
    float SpellRadius;
    float SpellLength;
    #endregion .

    private void Start()
    {
        foreach (Card c in CardManager.Instance.AllCards)
        {
            GameObject o = Instantiate(button, transform);
            o.GetComponent<DEBUGCardHolder>().DisplayCard(c, this);
        }

        playerStats = Player.GetComponent<PlayerStats>();
        playerCombat = Player.GetComponent<PlayerCombat>();
        entityHealth = Player.GetComponent<EntityHealth>();

        RecordPlayerStats();
    }

    public void AddCard(Card c)
    {
        if (c is WeaponCard) { selectedCards.Insert(0, c); return; }
        selectedCards.Add(c);
    }

    public void RemoveCard(Card c)
    {
        if (selectedCards.Contains(c))
        {
            selectedCards.Remove(c);
        }
    }

    public void ApplyCards()
    {
        ResetPlayer();

        new CardLoader(Player).LoadEntityCards(selectedCards);
    }

    public void ResetCards()
    {
        ResetPlayer();

        foreach (DEBUGCardHolder b in transform.GetComponentsInChildren<DEBUGCardHolder>())
        {
            if (b.selected) { b.OnClick(); }
        }
    }

    private void RecordPlayerStats()
    {
        MaxHealth = entityHealth.MaxHealth;
        CurrentHealth = entityHealth.CurrentHealth;
        SpeedModifier = playerStats.SpeedModifier;
        Acceleration = playerStats.Acceleration;

        MeleeCooldown = playerCombat.PrimaryCooldownModifier;
        PrimaryDamage = playerCombat.PrimaryDamageModifer;
        PrimaryRadius = playerCombat.PrimaryRadiusModifier;

        SpellCost = playerCombat.SpellCostModifier;
        SpellDamage = playerCombat.SpellDamageModifer;
        SpellRadius = playerCombat.SpellRadiusModifier;
        SpellLength = playerCombat.SpellCooldownModifier;
    }

    private void ResetPlayer()
    {
        entityHealth.MaxHealth = MaxHealth;
        entityHealth.CurrentHealth = CurrentHealth;
        playerStats.SpeedModifier = SpeedModifier;
        playerStats.Acceleration = Acceleration;

        playerCombat.PrimaryCooldownModifier = MeleeCooldown;
        playerCombat.PrimaryDamageModifer = PrimaryDamage;
        playerCombat.PrimaryRadiusModifier = PrimaryRadius;
        playerCombat.PrimaryStatusEffects = new();

        playerCombat.SpellCostModifier = SpellCost;
        playerCombat.SpellDamageModifer = SpellDamage;
        playerCombat.SpellRadiusModifier = SpellRadius;
        playerCombat.SpellCooldownModifier = SpellLength;
        playerCombat.SpellStatusEffects = new();
    }
}
