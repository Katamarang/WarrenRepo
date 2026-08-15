using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public abstract class NEWWeaponSpell : NEWSpell
{
    [Header("Weapon")]
    [SerializeField] float baseCooldown;  
    protected bool inCooldown;
    float finalCooldown;

    [SerializeField] float baseRadius;
    protected float finalRadius;

    [SerializeField] int baseDamage;
    protected int finalDamage;

    public void OnEnable()
    {
        NEWPlayerInput.OnAttackStarted += OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        NEWPlayerInput.OnAttackStarted -= OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled -= OnAttackCanceled;
    }

    public virtual void OnAttackStarted(InputAction.CallbackContext context) 
    {
        finalRadius = NEWPlayerSpell.AdjustValue<IAdjustRadius>(baseRadius, x => x.AdjustRadius(), ModType.Weapon);
        finalDamage = Mathf.RoundToInt(NEWPlayerSpell.AdjustValue<IAdjustDamage>(baseDamage, x => x.AdjustDamage(), ModType.Weapon));
    }

    public virtual void OnAttackCanceled(InputAction.CallbackContext context) { }

    protected void OnHit(EntityHealth entity)
    {
        entity.TakeDamage(finalDamage);

        List<StatusEffect> effects = new List<StatusEffect>();
        foreach (IApplyStatus status in NEWPlayerSpell.GetSpellsOfType<IApplyStatus>(ModType.Weapon))
        {
            StatusEffect effect = status.ApplyStatusEffect();
            if (effect == null) continue;

            effects.Add(effect);
        }

        entity.ApplyStatusEffect(effects);
    }

    protected void BeginCooldown()
    {
        inCooldown = true;

        finalCooldown = NEWPlayerSpell.AdjustValue<IAdjustCooldown>(baseCooldown, x => x.AdjustCooldown(), ModType.Weapon);
    }

    private void Update()
    {
        if (!inCooldown) return;

        finalCooldown -= Time.deltaTime;
        if (finalCooldown < 0) inCooldown = false;
    }
}
