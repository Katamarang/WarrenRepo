using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats
{
    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Health")]
    public int MaxHealth;
    public int CurrentHealth;

    [Header("Melee Combat")]
    public int MeleeDamage = 1;
    public float MeleeCooldown = 0.3f;
    public float ComboCooldown = 0.2f;
    public List<DamageType> MeleeDamageTypes = new List<DamageType>();
    public WeaponBehaviour MeleeBehaviour;

    [Header("Parry")]
    public float ParryWindow = 0.6f;

    [Header("Spell")]
    public int SpellCost;
    [HideInInspector] public int SpellCharges;
    public int SpellDamage;
    public float SpellRadius;
    public List<DamageType> SpellDamageType;
    public WeaponBehaviour SpellBehaviour; 

    [Space(25)]
    public Transform AttackPoint;
    public float AttackRadius;

    [Space(25)]
    
    public List<DamageType> DamageResistances = new List<DamageType>();
    public LayerMask Damageable;

    List<Card> Cards;
    CardLoader CardLoader;

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
        Cards = cards;
        print("Cards Recieved");
        LoadCardLoader();
    }

    public void LoadCardLoader()
    {
        CardLoader = new CardLoader(this);
        CardLoader.LoadCards(Cards);
    }
}
