using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EntityInput : Entity
{
    public UnityAction<InputAction.CallbackContext> OnMovePressed;
    public UnityAction OnMoveCanceled;

    public UnityAction OnAttackStarted;
    public UnityAction OnAttackCanceled;

    public UnityAction OnParryPressed;

    public UnityAction OnSpellPressed;
}
