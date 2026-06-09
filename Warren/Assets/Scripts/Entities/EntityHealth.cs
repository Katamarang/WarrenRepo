using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    // class for controlling the health of an entity, and applying status effects to it.

    public EntityStats EntityStats {  get; private set; }
    public StateMachine EntityStateMachine { get; private set; }

    public int CurrentHealth = 50;

    List<StatusEffect> activeStatus = new List<StatusEffect>();
    [SerializeField] Transform statusUI;
    

    public void Load()
    {
        EntityStats = GetComponent<EntityStats>();
        EntityStateMachine = GetComponent<StateMachine>();

        CurrentHealth = EntityStats.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        OnDeath();  
        
        if (FindFirstObjectByType<DEBUGDamageNumbers>() is DEBUGDamageNumbers debug) // debug
        {
            debug.DisplayDamageNumber(damage, DamageType.None);
        }
    }

    public void TakeDamage(int damage, StatusEffect effect)
    {
        CurrentHealth -= damage;
        OnDeath();

        if (FindFirstObjectByType<DEBUGDamageNumbers>() is DEBUGDamageNumbers debug) // debug
        {
            debug.DisplayDamageNumber(damage, effect.DamageType);
        }
    }

    public void ApplyStatusEffect(int damage, List<StatusEffect> appliedStatus)
    {
        foreach (var effect in appliedStatus)
        {
            if (!activeStatus.Contains(effect))
            {
                activeStatus.Add(effect);
                effect.OnStatusApplied(this, statusUI, 0);
            }
        }
    }

    private void UpdateStatusEffects() // calls OnStatusUpdate for each active status effect, and removes it if it returns true
    {
        if (activeStatus.Count == 0) { return; }

        List<StatusEffect> effects = activeStatus; // creates a cache of applied status effects

        foreach (var effect in effects.ToList()) // removes the status effect once it finishes
        {
            if (effect.OnStatusUpdate()) { activeStatus.Remove(effect); }
        }
    }

    public void OnDeath()
    {
        if (CurrentHealth <= 0) { print(gameObject.name + " Death"); }
    }


    private void Update()
    {
        UpdateStatusEffects();
    }

    

}
