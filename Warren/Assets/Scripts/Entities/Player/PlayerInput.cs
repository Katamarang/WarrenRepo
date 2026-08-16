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

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += (c) => OnMovePressed?.Invoke(c);
        inputActions.Player.Move.canceled += (c) => OnMoveCanceled?.Invoke(c);

        inputActions.Player.Attack.started += (c) => OnAttackStarted?.Invoke(c);
        inputActions.Player.Attack.canceled += (c) => OnAttackCanceled?.Invoke(c);
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= (c) => OnMovePressed?.Invoke(c);
        inputActions.Player.Move.canceled -= (c) => OnMoveCanceled?.Invoke(c);

        inputActions.Player.Attack.started -= (c) => OnAttackStarted?.Invoke(c);
        inputActions.Player.Attack.canceled -= (c) => OnAttackCanceled?.Invoke(c);

        inputActions.Disable();
    }
}
