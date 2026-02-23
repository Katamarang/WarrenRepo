using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SM_Player : StateMachine
{
    public float MaxSpeed;
    public float Acceleration;

    public PlayerInput PlayerInput { get; private set; }
    public Rigidbody2D RB {  get; private set; }

    public Weapon Weapon;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();

        Initialize(new P_IdleState(this));
    } 
}
