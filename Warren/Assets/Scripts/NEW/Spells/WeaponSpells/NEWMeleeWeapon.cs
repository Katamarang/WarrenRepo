using UnityEngine;
using UnityEngine.InputSystem;

public class NEWMeleeWeapon : NEWWeaponSpell
{
    public override void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (inCooldown) return;

        base.OnAttackStarted(context);
        BeginCooldown();

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(transform.position, finalRadius))
        {
            if (!hit.TryGetComponent<EntityHealth>(out EntityHealth entity)) continue;

            OnHit(entity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, finalRadius);
    }

}
