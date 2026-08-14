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
            if (!hit.TryGetComponent<EntityHealth>(out EntityHealth health)) continue;

            health.TakeDamage(finalDamage);

            // TO DO: Handle Status effects
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, finalRadius);
    }

}
