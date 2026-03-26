using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public GameObject StatusParticle;

    public int Damage;
    public float Length;

    public abstract void OnStatusApplied(EntityHealth entity);
    public abstract bool OnStatusUpdate();
    public abstract void OnStatusEnd();
}
