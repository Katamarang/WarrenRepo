using UnityEngine;

[CreateAssetMenu(fileName = "new Burning Status", menuName = "Scriptable Objects/Status Effect/Burning")]
public class BurningStatus : StatusEffect
{
    [Header("Burning")]
    public float DamageTickSpeed;

    float damageTickTime;
    float statusLengthTime;

    EntityHealth entity;
    bool resistant;

    public override void OnStatusApplied(EntityHealth entity)
    {
        this.entity = entity;
        if (entity.EntityStats is PlayerStats stats && stats.DamageResistances.Contains(this)) resistant = true;
        Debug.Log("Burning Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (statusLengthTime > Length) { OnStatusEnd(); return true; }
        statusLengthTime += Time.deltaTime;

        if (damageTickTime > DamageTickSpeed)
        {
            entity.TakeDamage(!resistant? Damage : Damage/2);
            damageTickTime = 0;
        } 
        else 
        { 
            damageTickTime += Time.deltaTime; 
        }

        return false;
    }

    public override void OnStatusEnd()
    {
        damageTickTime = 0;
        statusLengthTime = 0;

        resistant = false;
    }

    
}
