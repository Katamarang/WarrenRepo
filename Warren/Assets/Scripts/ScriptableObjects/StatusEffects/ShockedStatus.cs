using UnityEngine;

[CreateAssetMenu(fileName = "new Shocked Status", menuName = "Scriptable Objects/Status Effect/Shocked")]
public class ShockedStatus : StatusEffect
{
    [Header("Shocked")]
    public float DamageDelay;
    public float StunTime;

    float damageDelayTime;

    public override void OnStatusApplied(EntityHealth entity, Transform UIElement, int index)
    {
        base.OnStatusApplied(entity, UIElement, 2); // Gets shocked ui and enables it

        Debug.Log("Shocked Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (base.OnStatusUpdate()) { return true;}
        return false;
    }

    public override void OnStatusEnd()
    {
        entity.TakeDamage(!resistant? Damage : Damage / 2, this);

        //entity.EntityStateMachine.TransitionTo(entity.EntityStateMachine.StunState);
        base.OnStatusEnd();
    }

    
}
