using UnityEngine;

[CreateAssetMenu(fileName = "new Melee Card", menuName = "Scriptable Objects/Cards/Weapons/Melee Weapon")]
public class MeleeCard : WeaponCard { } // class used for organisation

public class MeleeBehaviour : WeaponBehaviour
{
    //WeaponBehaviours behaviours = WeaponBehaviours.Melee;

    public override void OnFire()
    {
        //Debug.Log("Attack");

        //Collider2D[] raycast = Physics2D.OverlapCircleAll(pos, radius, mask);

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(pos.position, radius, mask))
        {
            EntityHealth h = hit.GetComponent<EntityHealth>();

            h.TakeDamage(damage);
            if (damageTypes.Count != 0) { h.ApplyStatusEffect(damage, damageTypes); }          
        }
    }
}
