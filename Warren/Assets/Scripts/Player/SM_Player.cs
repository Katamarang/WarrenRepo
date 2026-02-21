using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SM_Player : StateMachine
{
    public float MaxSpeed;
    public float Acceleration;

    public Vector2 InputDirection {  get; private set; }
    public Rigidbody2D RB {  get; private set; }


    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        Initialize(new P_IdleState(this));
    }

    public void OnMove(InputValue input)
    {
        InputDirection = input.Get<Vector2>();
    } 
}
