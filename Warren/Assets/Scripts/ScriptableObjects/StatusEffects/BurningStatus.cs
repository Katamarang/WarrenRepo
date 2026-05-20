using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Burning Status", menuName = "Scriptable Objects/Status Effect/Burning")]
public class BurningStatus : StatusEffect
{
    [Header("Burning")]
    public float DamageTickSpeed;

    float damageTickTime;

    public override void OnStatusApplied(EntityHealth entity, Transform UIElement, int index)
    {
        base.OnStatusApplied(entity, UIElement, 0);

        Debug.Log("Burning Applied");
    }

    public override bool OnStatusUpdate()
    {
        if (base.OnStatusUpdate()) { return true; }

        if (damageTickTime > DamageTickSpeed)
        {
            entity.TakeDamage(!resistant? Damage : Damage/2, this);
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
        base.OnStatusEnd();
    }

    
}
