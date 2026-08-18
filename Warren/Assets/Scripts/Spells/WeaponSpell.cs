using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public abstract class WeaponSpell : AbilitySpell
{
    [Header("Weapon")]
    [SerializeField] protected LayerMask IsAttacking;
    [SerializeField] float baseRadius;
    protected float finalRadius;

    [SerializeField] int baseDamage;
    protected int finalDamage;

    List<IApplyStatus> equipedStatuses;

    public override void OnEnabled()
    {
        base.OnEnabled();
        EntityInput.OnAttackStarted += OnAbilityStarted;
        EntityInput.OnAttackCanceled += OnAbilityEnded;      
    }

    public override void OnDisabled()
    {
        base.OnDisabled();      
        EntityInput.OnAttackStarted -= OnAbilityStarted;
        EntityInput.OnAttackCanceled -= OnAbilityEnded;
    }

    public override void UpdateValues()
    {
        base.UpdateValues();

        finalRadius = EntitySpell.AdjustValue(baseRadius, AbilityType, StatType.Radius);
        finalDamage = Mathf.RoundToInt(EntitySpell.AdjustValue(baseDamage, AbilityType, StatType.Damage));

        equipedStatuses = EntitySpell.GetModifierSpellsOfType<IApplyStatus>(ModType.Weapon, StatType.Status);
    }

    protected void OnHit(Hurtbox hurtbox)
    {
        hurtbox.OnHit(finalDamage, equipedStatuses);
    }

}
