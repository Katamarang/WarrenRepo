using Unity.VisualScripting;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    // superclass for all status effects. Contains the base functions and variables.
    public GameObject StatusParticle;

    public int Damage;
    public float Length;
    public DamageType DamageType;

    internal EntityHealth entity;
    internal GameObject UIElement;

    internal float statusLengthTime;
    internal bool resistant;
    internal bool vunerable;

    // called when the status is applied to an entity.
    public virtual void OnStatusApplied(EntityHealth entity, Transform UIElement, int index) 
    {
        this.entity = entity;
        this.UIElement = UIElement.GetChild(index).gameObject;

        // may break once enemy stats are added. 'this' might be refering to this class and not the subclass
        if (entity.EntityStats is PlayerStats pstats && pstats.ElementType.Contains(this)) resistant = true; 
        else if (entity.EntityStats is EnemyStats estats && estats.ElementType.Contains(this)) vunerable = true;

        // applies the UI element to the target
        this.UIElement.SetActive(true);
    }
    public virtual bool OnStatusUpdate() // update method for status. Returns true once the status effect is finished.
    {
        if (statusLengthTime > Length) { OnStatusEnd(); return true; }
        statusLengthTime += Time.deltaTime;

        return false;
    }
    public virtual void OnStatusEnd() // called when the status effect is finished.
    {
        UIElement.SetActive(false);
        resistant = false;
        vunerable = false;

        statusLengthTime = 0;
    }

}
