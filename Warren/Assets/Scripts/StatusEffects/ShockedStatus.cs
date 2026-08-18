using UnityEngine;

public class ShockedStatus : StatusEffect
{
    [Header("Lightning")]
    [SerializeField] int damage;

    public override void OnStatusReapplied()
    {
        OnStatusEnd();
    }

    public override void OnStatusEnd()
    {
        EntityHealth.TakeDamage(damage);

        base.OnStatusEnd();
    }
}
