using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Combat")]
    public int MeleeDamage = 1;
    public float MeleeCooldown = 0.3f;
    public float ComboCooldown = 0.2f;
    public DamageType DamageType = DamageType.None;

    [Space(25)]
    public float ParryWindow = 0.6f;

    CardManager CardManager;
    List<Card> Cards;
    Animator Animator;

    #region Initialization
    private void Awake()
    {
        CardManager = CardManager.Instance;
        CardManager.SendPlayerCards += OnPlayerCardsRecieved;

        Animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        CardManager.SendPlayerCards -= OnPlayerCardsRecieved;
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
            if (card is WeaponCard)
            {
                LoadWeaponCards(card as WeaponCard);
            }
            //other card types go here
        }
    }

    private void LoadWeaponCards(WeaponCard weaponCard) // loads weapon Cards and apply their stats 
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

    
}
