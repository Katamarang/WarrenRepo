using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Spells/Abilities/Weapons/Melee Weapon")]
public class MeleeWeapon : WeaponSpell
{
    public override async void OnAbilityStarted()
    {
        if (AbilityActive()) return;
        
        base.OnAbilityStarted();
        
        foreach (Collider2D hit in Physics2D.OverlapCircleAll(EntitySpell.transform.position, finalRadius, IsAttacking))
        {
            if (!hit.TryGetComponent<Hurtbox>(out Hurtbox hurtbox)) continue;

            OnHit(hurtbox);
        }

        await BeginCooldown();
    }
}
