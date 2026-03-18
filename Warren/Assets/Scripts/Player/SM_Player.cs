using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SM_Player : StateMachine
{
    #region References
    public PlayerInput PlayerInput { get; private set; }
    public Rigidbody2D RB {  get; private set; }
    public Animator Animator { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    #endregion

    #region States
    public P_IdleState IdleState { get; private set; }
    public P_WalkState WalkState { get; private set; }
    public P_AttackState AttackState { get; private set; }
    public P_ParryState ParryState { get; private set; }
    public P_SpellState SpellState { get; private set; }
    #endregion

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();
        PlayerStats = GetComponent<PlayerStats>();

        CreateStates();

        Initialize(IdleState);
    }

    private void CreateStates()
    {
        IdleState = new(this, PlayerStats);
        WalkState = new(this, PlayerStats);
        AttackState = new(this, PlayerStats);
        ParryState = new(this, PlayerStats); 
        SpellState = new(this, PlayerStats);
    }
}
