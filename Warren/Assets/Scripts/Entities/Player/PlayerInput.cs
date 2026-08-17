using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputActions inputActions;

    public static UnityAction<InputAction.CallbackContext> OnMovePressed;
    public static UnityAction<InputAction.CallbackContext> OnMoveCanceled;

    public static UnityAction<InputAction.CallbackContext> OnAttackStarted;
    public static UnityAction<InputAction.CallbackContext> OnAttackCanceled;
    
    public static UnityAction<InputAction.CallbackContext> OnParryPressed;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        InputActions.PlayerActions Player = inputActions.Player;

        Player.Move.performed += (c) => OnMovePressed?.Invoke(c);
        Player.Move.canceled += (c) => OnMoveCanceled?.Invoke(c);

        Player.Attack.started += (c) => OnAttackStarted?.Invoke(c);
        Player.Attack.canceled += (c) => OnAttackCanceled?.Invoke(c);

        Player.Parry.started += (c) => OnParryPressed?.Invoke(c);
    }

    private void OnDisable()
    {
        InputActions.PlayerActions Player = inputActions.Player;

        Player.Move.performed -= (c) => OnMovePressed?.Invoke(c);
        Player.Move.canceled -= (c) => OnMoveCanceled?.Invoke(c);

        Player.Attack.started -= (c) => OnAttackStarted?.Invoke(c);
        Player.Attack.canceled -= (c) => OnAttackCanceled?.Invoke(c);

        Player.Parry.started -= (c) => OnParryPressed?.Invoke(c);

        inputActions.Disable();
    }
}
