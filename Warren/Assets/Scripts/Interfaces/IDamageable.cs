using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public abstract void TakeDamage(int damage);

    public abstract void OnDeath();
}
