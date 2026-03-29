using Unity.VisualScripting;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public GameObject StatusParticle;

    public int Damage;
    public float Length;

    internal EntityHealth entity;
    internal GameObject UIElement;

    internal float statusLengthTime;
    internal bool resistant;

    public virtual void OnStatusApplied(EntityHealth entity, Transform UIElement, int index)
    {
        this.entity = entity;
        this.UIElement = UIElement.GetChild(index).gameObject;

        // may break once enemy stats are added. 'this' might be refering to this class and not the subclass
        if (entity.EntityStats is PlayerStats stats && stats.DamageResistances.Contains(this)) resistant = true; 

        this.UIElement.SetActive(true);
    }
    public virtual bool OnStatusUpdate()
    {
        if (statusLengthTime > Length) { OnStatusEnd(); return true; }
        statusLengthTime += Time.deltaTime;

        return false;
    }
    public virtual void OnStatusEnd()
    {
        UIElement.SetActive(false);
        resistant = false;
        statusLengthTime = 0;
    }

}
