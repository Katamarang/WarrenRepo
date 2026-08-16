using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : WeaponSpell
{
    public override async void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (inCooldown) return;

        base.OnAttackStarted(context);
        
        foreach (Collider2D hit in Physics2D.OverlapCircleAll(transform.position, finalRadius))
        {
            if (!hit.TryGetComponent<EntityHealth>(out EntityHealth entity)) continue;

            OnHit(entity);
        }

        await BeginCooldown();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, finalRadius);
    }

}
