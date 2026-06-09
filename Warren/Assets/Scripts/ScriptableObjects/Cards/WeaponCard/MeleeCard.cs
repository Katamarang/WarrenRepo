using UnityEngine;

[CreateAssetMenu(fileName = "new Melee Card", menuName = "Scriptable Objects/Cards/Weapons/Melee Weapon")]
public class MeleeCard : WeaponCard { } // class used for organisation

public class MeleeBehaviour : WeaponBehaviour // subclass of WeaponBehaviour, used to define the behaviour of a melee weapon
{

    public override void OnFire()
    {
        // will check for all colliders in the radius and apply damage and status effects to them.
        foreach (Collider2D hit in Physics2D.OverlapCircleAll(pos.position, radius, mask))
        {
            EntityHealth h = hit.GetComponent<EntityHealth>();

            h.TakeDamage(damage);
            if (damageTypes.Count != 0) { h.ApplyStatusEffect(damage, damageTypes); }          
        }
    }
}
