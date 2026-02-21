using UnityEngine;

public class P_WalkState : IState
{
    SM_Player _player;

    float _currentSpeed;

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
        if(_currentSpeed < maxSpeed) { _currentSpeed += acceleration * Time.deltaTime; }
        else { _currentSpeed = maxSpeed; }

        rb.linearVelocity = _player.InputDirection * _currentSpeed;

        Transition();
    }

    private void Transition()
    {
        if (_player.InputDirection == Vector2.zero)
        {
            _player.TransitionTo(new P_IdleState(_player));
        }
    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        _currentSpeed = 0;
    }
}
