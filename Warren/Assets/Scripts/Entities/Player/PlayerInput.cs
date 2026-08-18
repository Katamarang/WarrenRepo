using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : EntityInput
{
    InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        InputActions.PlayerActions Player = inputActions.Player;

        Player.Move.performed += (c) => OnMovePressed?.Invoke(c);
        Player.Move.canceled += (c) => OnMoveCanceled?.Invoke();

        Player.Attack.started += (c) => OnAttackStarted?.Invoke();
        Player.Attack.canceled += (c) => OnAttackCanceled?.Invoke();

        Player.Parry.started += (c) => OnParryPressed?.Invoke();

        Player.Spell.started += (c) => OnSpellPressed?.Invoke();
    }

    private void OnDisable()
    {
        InputActions.PlayerActions Player = inputActions.Player;

        Player.Move.performed -= (c) => OnMovePressed?.Invoke(c);
        Player.Move.canceled -= (c) => OnMoveCanceled?.Invoke();

        Player.Attack.started -= (c) => OnAttackStarted?.Invoke();
        Player.Attack.canceled -= (c) => OnAttackCanceled?.Invoke();

        Player.Parry.started -= (c) => OnParryPressed?.Invoke();

        Player.Spell.started -= (c) => OnSpellPressed?.Invoke();

        inputActions.Disable();
    }
}
