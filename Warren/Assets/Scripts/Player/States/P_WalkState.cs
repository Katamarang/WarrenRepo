using UnityEngine;

public class P_WalkState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    float _currentSpeed;
    int _inputBuffer;
    Vector2 _inputDirection;

    // player variabes
    Rigidbody2D rb;
    Animator anim;
    float maxSpeed;
    float acceleration;

    public P_WalkState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

        rb = player.RB;
        anim = player.Animator;
        maxSpeed = playerStats.MaxSpeed;
        acceleration = playerStats.Acceleration;
    }

    public void Enter()
    {
        anim.SetBool("IsRunning", true);
    }

    public void Update()
    {
        _inputDirection = _player.PlayerInput.ReadInput();
        anim.SetFloat("PosY", Mathf.RoundToInt(_player.PlayerInput.PlayerFacing.y));
        anim.SetFloat("PosX", Mathf.RoundToInt(_player.PlayerInput.PlayerFacing.x));


        if (_inputDirection != Vector2.zero) _player.transform.localScale = new Vector2(_inputDirection.x >= 0f ? -1 : 1, 1);
        Transition(_inputDirection);       
    }

    public void FixedUpdate()
    {
        if (_currentSpeed < _playerStats.MaxSpeed) { _currentSpeed += _playerStats.Acceleration * Time.deltaTime; }
        else { _currentSpeed = _playerStats.MaxSpeed; }

        rb.linearVelocity = _inputDirection * _currentSpeed;
    }

    private void Transition(Vector2 dir)
    {
        //movement
        if (dir == Vector2.zero)
        {
            if (_inputBuffer < 6) { _inputBuffer++; _currentSpeed = 0; return; }

            _player.TransitionTo(_player.IdleState);
        } 
        else { _inputBuffer = 0; }

        //attack
        if (_player.PlayerInput.Attack())
        {
            _player.TransitionTo(_player.AttackState);
        }

        //parry
        if (_player.PlayerInput.Parry())
        {
            _player.TransitionTo(_player.ParryStartState);
        }

    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        _currentSpeed = 0;

        anim.SetBool("IsRunning", false);
    }
}
