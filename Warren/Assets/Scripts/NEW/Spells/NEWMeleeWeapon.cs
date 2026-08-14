using UnityEngine;
using UnityEngine.InputSystem;

public class NEWMeleeWeapon : NEWWeaponSpell
{
    public override void OnAttackStarted(InputAction.CallbackContext context)
    {
        print("boo");
    }

    public override void OnAttackCanceled(InputAction.CallbackContext context)
    {
        print("brr");
    }
}
