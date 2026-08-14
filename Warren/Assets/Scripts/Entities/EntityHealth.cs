using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityHealth : Entity
{
    // class for controlling the health of an entity, and applying status effects to it.

    [Header("Health")]
    public int MaxHealth;
    public int CurrentHealth = 50;

    [Header("Elements")]
    public List<StatusEffect> StatusResistant = new List<StatusEffect>();
    public List<StatusEffect> StatusVunerable = new List<StatusEffect>();

    List<StatusEffect> activeStatus = new List<StatusEffect>();
    [SerializeField] Transform statusUI;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Load()
    {

        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage, DamageType effect = DamageType.None)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) { print(gameObject.name + " Death"); OnDeath(); }

        if (FindFirstObjectByType<DEBUGDamageNumbers>() is DEBUGDamageNumbers debug) // debug
        {
            debug.DisplayDamageNumber(damage, effect);
        }
    }

    public void ApplyStatusEffect(List<StatusEffect> appliedStatus)
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
        
    }


    private void Update()
    {
        UpdateStatusEffects();
    }

    

}
