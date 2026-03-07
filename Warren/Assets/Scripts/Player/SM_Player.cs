using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SM_Player : StateMachine
{
    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;

    [Header("Parry")]
    public float ParryWindow;

    [Header("Weapons")]
    public Weapon Weapon;
    //public Parry Parry;
    //public Dash Dash;

    #region References
    public PlayerInput PlayerInput { get; private set; }
    public Rigidbody2D RB {  get; private set; }
    public Animator Animator { get; private set; }
    #endregion
    
    #region Player States
    public IState IdleState { get; private set; }
    public IState WalkState { get; private set; }
    public IState AttackState { get; private set; }
    public IState ParryStartState { get; private set; }
    // other states, eventually
    #endregion

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();

        InitialiseStates();

        Initialize(IdleState);
    } 

    private void InitialiseStates()
    {
        IdleState = new P_IdleState(this);
        WalkState = new P_WalkState(this);
        AttackState = new P_AttackState(this);
        ParryStartState = new P_ParryInitState(this);
    }
}
