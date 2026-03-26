using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeBehaviour", menuName = "Scriptable Objects/Weapon Behaviour/Melee")]
public class MeleeBehaviour : WeaponBehaviour
{
    public override List<IDamageable> OnFire(Transform center, float radius, LayerMask mask)
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(center.position, radius, mask);
        List<IDamageable> damageables = new List<IDamageable>();

        foreach (Collider2D r in results)
        {
            if (r.GetComponent<IDamageable>() != null)
            {
                damageables.Add(r.GetComponent<IDamageable>());
            }
        }

        //Debug.Log("Hit Swung");
        return damageables;
    }
}
