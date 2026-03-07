using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void OnAttack()
    {
        Anim.SetTrigger("IsAttacking");
    }
}
