using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public DamageType DamageType;
    [SerializeField] protected float effectLength;
    
    protected EntityHealth EntityHealth; // the target of the status effect
    protected float time;

    public virtual void OnStatusApplied(EntityHealth entity)
    {
        EntityHealth = entity;
    }

    public virtual void OnStatusReapplied() { }

    private void Update()
    {
        OnStatusUpdate();
    }

    public virtual void OnStatusUpdate()
    {
        if (time > effectLength) { OnStatusEnd(); return; }
        time += Time.deltaTime;
    }

    public virtual void OnStatusEnd()
    {
        time = 0;
        gameObject.SetActive(false);
    }
}
