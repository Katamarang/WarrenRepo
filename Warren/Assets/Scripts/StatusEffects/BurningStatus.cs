using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BurningStatus : StatusEffect
{
    [Header("Burning")]
    [SerializeField] int damage;
    [SerializeField] float damageTickSpeed;
    float damageTickTime;

    public override void OnStatusReapplied()
    {
        time = 0f;
    }

    public override void OnStatusUpdate()
    {
        base.OnStatusUpdate();

        if (damageTickTime > damageTickSpeed)
        {
            EntityHealth.TakeDamage(damage, DamageType);
            damageTickTime = 0;
        }
        else
        {
            damageTickTime += Time.deltaTime;
        }
    }

    public override void OnStatusEnd()
    {
        damageTickTime = 0;
        base.OnStatusEnd();
    }
}
