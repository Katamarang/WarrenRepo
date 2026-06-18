using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "new Poison Status", menuName = "Scriptable Objects/Status Effect/Poison")]
public class PoisonStatus : StatusEffect
{
    // Poison status effect. Decreases the target's speed and damage for a duration.
    [Header("Poison")]
    public float SpeedDecrease;
    public int DamageDecrease;

    float defaultSpeed;
    int defaultdamage;

    EntityStats stats;
    EntityCombat combat;

    public override void OnStatusApplied(EntityHealth entity, Transform UIElement, int index)
    {
        base.OnStatusApplied(entity, UIElement, 1);
        stats = entity.GetComponent<EntityStats>();
        combat = entity.GetComponent<EntityCombat>();

        defaultSpeed = stats.SpeedModifier;
        defaultdamage = combat.PrimaryDamageModifer;

        stats.MaxSpeed -= SpeedDecrease;
        combat.PrimaryDamageModifer -= DamageDecrease;

        Debug.Log("Poison Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (base.OnStatusUpdate()) { return true; }
        return false;
    }

    public override void OnStatusEnd()
    {
        stats.SpeedModifier = defaultSpeed;
        combat.PrimaryDamageModifer = defaultdamage;

        defaultSpeed = 0;
        defaultdamage = 0;

        base.OnStatusEnd();
    }
    
}
