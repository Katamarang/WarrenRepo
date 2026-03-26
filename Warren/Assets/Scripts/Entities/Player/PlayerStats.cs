using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats
{
    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Health")]
    public int MaxHealth;

    [Header("Melee Combat")]
    public int MeleeDamage = 1;
    public float AttackRadius = 0.5f;
    public float MeleeCooldown = 0.3f;
    public float ComboCooldown = 0.2f;
    public List<StatusEffect> MeleeDamageTypes = new List<StatusEffect>();
    public WeaponBehaviour MeleeBehaviour;

    [Header("Parry")]
    public float ParryWindow = 0.6f;

    [Header("Spell")]
    public int SpellCost;
    public int SpellDamage;
    public float SpellRadius;
    public float SpellLength;
    public List<StatusEffect> SpellDamageTypes = new List<StatusEffect>();
    public WeaponBehaviour SpellBehaviour; 

    [Space(25)]
    public Transform AttackPoint;
 
    [Space(25)]
    
    public List<StatusEffect> DamageResistances = new List<StatusEffect>();
    public LayerMask Damageable;
    public Animator Animator;

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
        GetComponent<EntityHealth>().Load();
    }

    public void LoadCardLoader()
    {
        CardLoader = new CardLoader(this);
        CardLoader.LoadPlayerCards(Cards);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }
}
