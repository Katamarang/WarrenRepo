using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class StatusEffect : MonoBehaviour
{
    [SerializeField] GameObject UIElement;
    public DamageType DamageType;
    [SerializeField] protected float effectLength;
    
    protected EntityHealth EntityHealth; // the target of the status effect
    protected float time;

    public virtual void OnStatusApplied(EntityHealth entity)
    {
        EntityHealth = entity;
        UIElement.SetActive(true);
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
        UIElement.SetActive(false);
    }
}
