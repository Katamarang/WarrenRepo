using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageable, IStatus
{
    public IStats EntityStats {  get; private set; }
    public StateMachine EntityStateMachine { get; private set; }

    public int CurrentHealth = 50;

    List<StatusEffect> activeStatus = new List<StatusEffect>();
    

    public void Load()
    {
        EntityStats = GetComponent<IStats>();
        EntityStateMachine = GetComponent<StateMachine>();

        if (EntityStats is PlayerStats player) { CurrentHealth = player.MaxHealth; }
        else if (EntityStats is EnemyStats enemy) { CurrentHealth = enemy.MaxHealth; }
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
                effect.OnStatusApplied(this);
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

        List<StatusEffect> effects = activeStatus;

        foreach (var effect in effects.ToList()) 
        {
            if (effect.OnStatusUpdate()) { activeStatus.Remove(effect); }
        }

    }

}
