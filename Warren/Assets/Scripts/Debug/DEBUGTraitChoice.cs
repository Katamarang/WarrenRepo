using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DEBUGTraitChoice : MonoBehaviour
{
    [SerializeField] GameObject button;

    [SerializeField] List<Card> selectedCards;

    EntityStats player;

    #region Stored Stats
    int MaxHealth;

    float MaxSpeed;
    float Acceleration;

    //int MeleeDamage = 1;
    //float AttackRadius = 0.5f;
    float MeleeCooldown = 0.3f;

    int SpellCost;
    int SpellDamage;
    float SpellRadius;
    float SpellLength;
    #endregion

    private void Start()
    {
        foreach (Card c in CardManager.Instance.AllCards)
        {
            GameObject o = Instantiate(button, transform);
            o.GetComponent<DEBUGCardHolder>().DisplayCard(c, this);
        }

        player = GameObject.Find("Player").GetComponent<EntityStats>();
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

        new CardLoader(player).LoadPlayerCards(selectedCards);
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
        MaxHealth = player.MaxHealth;
        MaxSpeed = player.MaxSpeed;
        Acceleration = player.Acceleration;
        //MeleeDamage = player.WeaponBehaviour.damage;
        //AttackRadius = player.WeaponBehaviour.radius;
        MeleeCooldown = player.MeleeCooldown;

        SpellCost = (player as PlayerStats).SpellCost;
        SpellDamage = (player as PlayerStats).SpellDamage;
        SpellRadius = (player as PlayerStats).SpellRadius;
        SpellLength = (player as PlayerStats).SpellLength;
    }

    private void ResetPlayer()
    {
        player.MaxHealth = MaxHealth;
        player.MaxSpeed = MaxSpeed;
        player.Acceleration = Acceleration;
        player.WeaponBehaviour = null;
        //player.WeaponBehaviour.damage = MeleeDamage;
        //player.WeaponBehaviour.radius = AttackRadius;
        player.MeleeCooldown = MeleeCooldown;
        //player.WeaponBehaviour.damageTypes = new();
        player.WeaponSlot.sprite = null;

        (player as PlayerStats).SpellCost = SpellCost;
        (player as PlayerStats).SpellDamage = SpellDamage;
        (player as PlayerStats).SpellRadius = SpellRadius;
        (player as PlayerStats).SpellLength = SpellLength;
        (player as PlayerStats).SpellDamageTypes = new();
    }
}
