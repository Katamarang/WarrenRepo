using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class NEWPlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 _direction;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        NEWPlayerInput.OnMovePressed += (t) => _direction = t.ReadValue<Vector2>().normalized;
        NEWPlayerInput.OnMoveCanceled += (t) => _direction = Vector2.zero;
    } 

    private void OnDisable()
    {
        NEWPlayerInput.OnMovePressed -= (t) => _direction = t.ReadValue<Vector2>().normalized;
        NEWPlayerInput.OnMoveCanceled -= (t) => _direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = _direction * speed;
    }
}
