using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public abstract class WeaponSpell : AbilitySpell
{
    [Header("Weapon")]
    [SerializeField] float baseRadius;
    protected float finalRadius;

    [SerializeField] int baseDamage;
    protected int finalDamage;

    List<IApplyStatus> equipedStatuses;

    private void OnEnable()
    {
        PlayerInput.OnAttackStarted += OnAbilityStarted;
        PlayerInput.OnAttackCanceled += OnAbilityEnded;

        PlayerSpell.UpdateValues += UpdateValues;
    }

    private void OnDisable()
    {
        PlayerInput.OnAttackStarted -= OnAbilityStarted;
        PlayerInput.OnAttackCanceled -= OnAbilityEnded;

        PlayerSpell.UpdateValues -= UpdateValues;
    }

    public override void UpdateValues()
    {
        base.UpdateValues();

        finalRadius = PlayerSpell.AdjustValue<IAdjustRadius>(baseRadius, x => x.AdjustRadius(), AbilityType);
        finalDamage = Mathf.RoundToInt(PlayerSpell.AdjustValue<IAdjustDamage>(baseDamage, x => x.AdjustDamage(), AbilityType));

        equipedStatuses = PlayerSpell.GetSpellsOfType<IApplyStatus>(ModType.Weapon);
    }

    protected void OnHit(EntityHealth entity)
    {
        entity.TakeDamage(finalDamage);

        foreach (IApplyStatus status in equipedStatuses)
        {
            entity.ApplyStatusEffect(status.ApplyStatusEffect());
        }      
    }

}
