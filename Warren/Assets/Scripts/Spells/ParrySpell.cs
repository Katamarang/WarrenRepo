using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParrySpell : AbilitySpell
{
    [Header("Parry")]
    public bool IsParrying;

    private void OnEnable() { PlayerInput.OnParryPressed += OnAbilityStarted; }

    private void OnDisable() { PlayerInput.OnParryPressed -= OnAbilityStarted; }

    public override async void OnAbilityStarted(InputAction.CallbackContext context)
    { 
        base.OnAbilityStarted(context);

        IsParrying = true;
        await BeginCooldown();
    }

    public override void OnAbilityEnd()
    {
        base.OnAbilityEnd();

        IsParrying = false;
    }

}
