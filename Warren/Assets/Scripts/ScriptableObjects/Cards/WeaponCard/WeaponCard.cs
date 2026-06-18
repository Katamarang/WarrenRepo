using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCard : PlayerCard
{
    // Weapon card specific properties
    public Sprite WeaponSprite;

    public float BaseAttackCooldown;
    public int BaseDamage;
    public float BaseAttackRadius;

    public WeaponType type;
    public int SpellCost;

    public abstract void OnFire(int damageMod, List<StatusEffect> statusEffects, Transform pos, LayerMask target);
}

public enum WeaponBehaviours
{
    Melee,
    Spawn
}

public enum WeaponType { Weapon, Spell }

public enum DamageType
{
    None,
    Fire, // Damage over time
    Poison, // Slows
    Lightning // high damage, has a few seconds charge up
}
