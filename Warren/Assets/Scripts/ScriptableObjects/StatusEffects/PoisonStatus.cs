using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "new Poison Status", menuName = "Scriptable Objects/Status Effect/Poison")]
public class PoisonStatus : StatusEffect
{
    [Header("Poison")]
    public float SpeedDecrease;
    public int DamageDecrease;

    float defaultSpeed;
    int defaultDamage;
    EntityStats stats;

    public override void OnStatusApplied(EntityHealth entity, Transform UIElement, int index)
    {
        base.OnStatusApplied(entity, UIElement, 1);
        stats = entity.EntityStats;

        defaultSpeed = stats.MaxSpeed;
        defaultDamage = stats.MeleeDamage;

        stats.MaxSpeed -= !resistant ? SpeedDecrease : SpeedDecrease / 2;
        stats.MeleeDamage -= !resistant ? DamageDecrease : DamageDecrease / 2;

        Debug.Log("Poison Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (base.OnStatusUpdate()) { return true; }
        return false;
    }

    public override void OnStatusEnd()
    {
        stats.MaxSpeed = defaultSpeed;
        stats.MeleeDamage = defaultDamage;

        defaultSpeed = 0;
        defaultDamage = 0;

        base.OnStatusEnd();
    }
    
}
