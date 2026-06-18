using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Spell")]
    public WeaponCard SpellCard;
    public int SpellCostModifier;
    public int SpellDamageModifer;
    public float SpellCooldownModifier;
    public float SpellRadiusModifier;
    public List<StatusEffect> SpellStatusEffects;
    public int ManaCharges;

    [Header("Parry")]
    public float ParryWindow = 0.8f;
}
