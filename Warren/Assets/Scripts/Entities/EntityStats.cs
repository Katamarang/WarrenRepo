using UnityEngine;
using System.Collections.Generic;

public class EntityStats : MonoBehaviour
{
    // class for controlling the stats of an entity.

    internal CardLoader cardLoader;
    internal List<Card> cards = new List<Card>();

    [Header("Health")]
    public int MaxHealth;

    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Melee Combat")]
    public float MeleeCooldown = 0.3f;
    public WeaponBehaviour WeaponBehaviour;

    [Header("Misc")]
    public List<StatusEffect> ElementType = new List<StatusEffect>(); // controls what the player is resistant to and what the enemies are vunereable to

    public Transform AttackPoint;
    public SpriteRenderer WeaponSlot;
    public LayerMask Damageable;

    [Space(15)]
    public Animator Animator;
}
