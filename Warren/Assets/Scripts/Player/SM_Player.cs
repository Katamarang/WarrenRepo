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
   

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();
        PlayerStats = GetComponent<PlayerStats>();
   
        Initialize(new P_IdleState(this, PlayerStats));
    } 

}
