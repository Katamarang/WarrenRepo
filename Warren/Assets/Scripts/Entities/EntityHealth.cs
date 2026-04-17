using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageable, IStatus
{
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

    public void OnDeath()
    {
        if (CurrentHealth <= 0) { print(gameObject.name + " Death"); }
    }


    private void Update()
    {
        if (activeStatus.Count == 0) { return; }

        List<StatusEffect> effects = activeStatus; // creates a cache of applied status effects

        foreach (var effect in effects.ToList()) // removes the status effect once it finishes
        {
            if (effect.OnStatusUpdate()) { activeStatus.Remove(effect); }
        }

    }

}
