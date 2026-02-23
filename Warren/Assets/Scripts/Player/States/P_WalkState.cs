using UnityEngine;

public class P_WalkState : IState
{
    SM_Player _player;

    float _currentSpeed;
    int _inputBuffer;
    Vector2 _inputDirection;

    // player variabes
    Rigidbody2D rb;
    float maxSpeed;
    float acceleration;

    public P_WalkState(SM_Player player)
    {
        _player = player;

        rb = player.RB;
        maxSpeed = player.MaxSpeed;
        acceleration = player.Acceleration;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        _inputDirection = _player.PlayerInput.ReadInput();
   
        Transition(_inputDirection);       
    }

    public void FixedUpdate()
    {
        if (_currentSpeed < maxSpeed) { _currentSpeed += acceleration * Time.deltaTime; }
        else { _currentSpeed = maxSpeed; }

        rb.linearVelocity = _inputDirection * _currentSpeed;
    }

    private void Transition(Vector2 dir)
    {
        //movement
        if (dir == Vector2.zero)
        {
            if (_inputBuffer < 6) { _inputBuffer++; _currentSpeed = 0; return; }

            _player.TransitionTo(new P_IdleState(_player));
        } 
        else { _inputBuffer = 0; }

        //attack
        if (_player.PlayerInput.Attack())
        {
            _player.TransitionTo(new P_AttackState(_player));
        }

    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        _currentSpeed = 0;
    }
}
