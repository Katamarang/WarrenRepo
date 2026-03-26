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

    float statusLengthTime;
    EntityHealth entity;
    bool resistant;

    public override void OnStatusApplied(EntityHealth entity)
    {
        this.entity = entity;
        if (entity.EntityStats is PlayerStats pstats && pstats.DamageResistances.Contains(this)) resistant = true;

        if (entity.EntityStats is PlayerStats stats)
        {
            defaultSpeed = stats.MaxSpeed;
            stats.MaxSpeed -= !resistant? SpeedDecrease : SpeedDecrease / 2;

            defaultDamage = stats.MeleeDamage;
            stats.MeleeDamage -= !resistant ? DamageDecrease : DamageDecrease / 2;
        } 
        else if (entity.EntityStats is EnemyStats estats)
        {
            defaultSpeed = estats.MaxSpeed;
            estats.MaxSpeed -= !resistant ? SpeedDecrease : SpeedDecrease / 2;

            defaultDamage = estats.Damage;
            estats.Damage -= !resistant ? DamageDecrease : DamageDecrease / 2;
        }
        Debug.Log("Poison Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (statusLengthTime > Length) { OnStatusEnd(); return true; }
        statusLengthTime += Time.deltaTime;

        return false;
    }

    public override void OnStatusEnd()
    {
        if (entity.EntityStats is PlayerStats stats)
        {
            stats.MaxSpeed = defaultSpeed;

            stats.MeleeDamage = defaultDamage;
        }
        else if (entity.EntityStats is EnemyStats estats)
        {
            estats.MaxSpeed = defaultSpeed;

            estats.Damage = defaultDamage;
        }

        statusLengthTime = 0;
        resistant = false;
    }
    
}
