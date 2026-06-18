using UnityEngine;
using System.Collections.Generic;

public class EntityCombat : Entity
{
    [Header("Primary Attack")]
    public WeaponCard PrimaryCard;
    public int PrimaryDamageModifer;
    public float PrimaryCooldownModifier;
    public float PrimaryRadiusModifier;
    public List<StatusEffect> PrimaryStatusEffects;

    [Header("Misc")]
    public Transform AttackPosition;
    public SpriteRenderer WeaponSlot;
    public LayerMask Damageable;
}
