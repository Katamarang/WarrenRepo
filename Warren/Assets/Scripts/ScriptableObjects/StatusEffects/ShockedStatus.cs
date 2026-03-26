using UnityEngine;

[CreateAssetMenu(fileName = "new Shocked Status", menuName = "Scriptable Objects/Status Effect/Shocked")]
public class ShockedStatus : StatusEffect
{
    [Header("Shocked")]
    public float DamageDelay;
    public float StunTime;

    float damageDelayTime;

    float statusLengthTime;

    EntityHealth entity;
    bool resistant;

    public override void OnStatusApplied(EntityHealth entity)
    {
        this.entity = entity;
        
        if (entity.EntityStats is PlayerStats stats && stats.DamageResistances.Contains(this)) resistant = true;
        Debug.Log("Shocked Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (statusLengthTime > Length) { OnStatusEnd(); return true; }
        statusLengthTime += Time.deltaTime;

        return false;
    }

    public override void OnStatusEnd()
    {
        statusLengthTime = 0;
        entity.TakeDamage(!resistant? Damage : Damage / 2);

        //entity.EntityStateMachine.TransitionTo(entity.EntityStateMachine.StunState);
        resistant = false;
    }

    
}
