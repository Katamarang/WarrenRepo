using UnityEngine;
using System.Collections.Generic;

public abstract class WeaponBehaviour : ScriptableObject
{
    // container class for weapons behaviours

    public abstract List<IDamageable> OnFire(Transform center, float radius, LayerMask mask);
}
