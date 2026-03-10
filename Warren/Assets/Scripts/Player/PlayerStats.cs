using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Health")]
    public int MaxHealth;
    public int CurrentHealth;

    [Header("Combat")]
    public int MeleeDamage = 1;
    public float MeleeCooldown = 0.3f;
    public float ComboCooldown = 0.2f;
    public List<DamageType> DamageTypes = new List<DamageType>();

    [Space(25)]
    public float ParryWindow = 0.6f;
    public List<DamageType> DamageResistances = new List<DamageType>();

    List<Card> Cards;
    Animator Animator;

    #region Initialization
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CardManager.Instance.SendPlayerCards += OnPlayerCardsRecieved;       
    }

    private void OnDestroy()
    {
        CardManager.Instance.SendPlayerCards -= OnPlayerCardsRecieved;
    }
    #endregion

    private void OnPlayerCardsRecieved(List<Card> cards)
    {
        Cards = cards;
        LoadCards();
    }

    private void LoadCards()
    {
        foreach (var card in Cards)
        {     
            if (card is WeaponCard) { LoadWeaponCard(card as WeaponCard); } 

            else if (card is WeaponModCard) { LoadWeaponModCard(card as WeaponModCard); }

            else if (card is StatModCard) { LoadStatModCard(card as StatModCard); }

            //other card types go here
        }
    }

    private void LoadWeaponCard(WeaponCard weaponCard) // loads weapon Cards and apply their stats 
    {
        if (weaponCard.WeaponType == WeaponType.Attack)
        {
            MeleeDamage = weaponCard.BaseDamage;
            MeleeCooldown = weaponCard.BaseAttackCooldown;

            transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponCard.WeaponSprite;
        }
        else if (weaponCard.WeaponType == WeaponType.Parry)
        {
            // do stuff
        }
        else if (weaponCard.WeaponType == WeaponType.Dash)
        {
            // do other stuff
        }
    }

    private void LoadWeaponModCard(WeaponModCard card) // loads weapon modification cards 
    {
        if (card.WeaponToAffect == WeaponType.Attack)
        {
            foreach (WeaponMod mod in card.WeaponModifiers)
            {
                if (mod.WeaponStat == WeaponStat.Damage) { MeleeDamage += (int)mod.Modifier; }
                else if (mod.WeaponStat == WeaponStat.Speed) 
                { 
                    MeleeCooldown -= mod.Modifier; 
                    if (MeleeCooldown < 0.1f) MeleeCooldown = 0.1f;
                }
                else if (mod.WeaponStat == WeaponStat.DamageType && !DamageTypes.Contains((DamageType)(int)mod.Modifier)) 
                {
                    DamageTypes.Add((DamageType)(int)mod.Modifier);
                }
            }
        }
        else if (card.WeaponToAffect == WeaponType.Parry)
        {
            // do stuff
        }
        else if (card.WeaponToAffect == WeaponType.Dash)
        {
            // do other stuff
        }
    }

    private void LoadStatModCard(StatModCard card) // loads player modifications 
    {
        foreach (ModifyStats stat in card.StatsToModify)
        {
            if (stat.Stat == PlayerGameStats.CurrentHealth) { CurrentHealth += stat.Modifier; }
            else if (stat.Stat == PlayerGameStats.MaxHealth) { MaxHealth += stat.Modifier; }
            else if (stat.Stat == PlayerGameStats.Speed) { MaxSpeed += stat.Modifier; }
            else if (stat.Stat == PlayerGameStats.Acceleration) { Acceleration += stat.Modifier; }
            else if (stat.Stat == PlayerGameStats.Resistance && !DamageResistances.Contains((DamageType)stat.Modifier)) 
                { DamageResistances.Add((DamageType)stat.Modifier); }

        }
    }
}
