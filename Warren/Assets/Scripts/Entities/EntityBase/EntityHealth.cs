using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityHealth : Entity
{
    // class for controlling the health of an entity, and applying status effects to it.

    [Header("Health")]
    public int MaxHealth;
    public int CurrentHealth = 50;

    Dictionary<DamageType, StatusEffect> Status;
    
    [SerializeField] Transform statusContainer;

    private void Awake()
    {
        CurrentHealth = MaxHealth;

        Status = new Dictionary<DamageType, StatusEffect>()
        {
            {DamageType.Fire, statusContainer.GetComponentInChildren<BurningStatus>(true) },
            {DamageType.Lightning, statusContainer.GetComponentInChildren<ShockedStatus>(true) },
            {DamageType.Poison, statusContainer.GetComponentInChildren<PoisonStatus>(true) },
        };
    }

    public void Load()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) { print(gameObject.name + " Death"); OnDeath(); }
    }

    public void ApplyStatusEffect(List<IApplyStatus> statuses)
    {
        foreach (IApplyStatus stat in statuses)
        {
            StatusEffect status = Status[stat.ApplyStatusEffect()];

            if (!status.gameObject.activeInHierarchy)
            {
                status.gameObject.SetActive(true);
                status.OnStatusApplied(this);
            }
            else
            {
                status.OnStatusReapplied();
            }
        }
    }

    public void OnDeath()
    {
        
    }  

}
