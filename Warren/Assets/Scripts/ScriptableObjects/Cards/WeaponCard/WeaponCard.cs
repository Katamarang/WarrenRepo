using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : PlayerCard
{
    // Weapon card specific properties
    public Sprite WeaponSprite;

    public float BaseAttackCooldown;
    public int BaseDamage;
    public float BaseAttackRadius;

    public WeaponBehaviours Behaviour;
}

public abstract class WeaponBehaviour // This is the base class for all weapon behaviours.
{
    public Transform pos;
    public float radius;
    public LayerMask mask;

    public int damage;
    public List<StatusEffect> damageTypes = new List<StatusEffect>();

    public abstract void OnFire();
}

public enum WeaponBehaviours
{
    Melee,
    Spawn
}

public enum DamageType
{
    None,
    Fire, // Damage over time
    Poison, // Slows
    Lightning // high damage, has a few seconds charge up
}
