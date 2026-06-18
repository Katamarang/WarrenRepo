using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Melee Card", menuName = "Scriptable Objects/Cards/Weapons/Melee Weapon")]
public class MeleeCard : WeaponCard 
{
    public override void OnFire(int damageModifier, List<StatusEffect> statusEffects, Transform pos, LayerMask target)
    {
        // will check for all colliders in the radius and apply damage and status effects to them.
        foreach (Collider2D hit in Physics2D.OverlapCircleAll(pos.position, BaseAttackRadius, target))
        {
            EntityHealth h = hit.GetComponent<EntityHealth>();

            h.TakeDamage(BaseDamage + damageModifier, DamageType.None);
            if (statusEffects.Count != 0) { h.ApplyStatusEffect(statusEffects); }
        }
    }
} 
