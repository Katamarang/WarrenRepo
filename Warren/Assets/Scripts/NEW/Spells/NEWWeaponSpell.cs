using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NEWWeaponSpell : NEWSpell
{
    [Header("Weapon")]
    public Sprite InGameSprite;
    public Animator Animator;

    public void OnEnable()
    {
        NEWPlayerInput.OnAttackStarted += OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        NEWPlayerInput.OnAttackStarted -= OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled -= OnAttackCanceled;
    }

    public abstract void OnAttackStarted(InputAction.CallbackContext context);
    public abstract void OnAttackCanceled(InputAction.CallbackContext context);
}
