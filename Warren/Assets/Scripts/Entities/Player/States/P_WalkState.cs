using UnityEngine;

public class P_WalkState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;
    PlayerInput _playerInput;

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
        _playerInput = _player.PlayerInput;

        rb = _player.RB;
        anim = _player.Animator;
    }

    public override void Enter()
    {
        anim.SetBool("IsRunning", true);
    
        maxSpeed = _playerStats.MaxSpeed;
        acceleration = _playerStats.Acceleration;
    }

    public override void Update()
    {
        _inputDirection = _playerInput.ReadInput();
        anim.SetFloat("PosY", Mathf.RoundToInt(_player.PlayerInput.PlayerFacing.y));
        anim.SetFloat("PosX", Mathf.RoundToInt(_player.PlayerInput.PlayerFacing.x));


        if (_inputDirection != Vector2.zero) _player.transform.localScale = new Vector2(_inputDirection.x >= 0f ? -1 : 1, 1);
        Transition(_inputDirection);       
    }

    public override void FixedUpdate()
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

            _player.TransitionTo(_player.IdleState);
        } 
        else { _inputBuffer = 0; }

        if (_playerInput.Attack()) { _player.TransitionTo(_player.AttackState); }
        else if (_playerInput.Parry()) { _player.TransitionTo(_player.ParryState); }
        else if (_playerInput.Spell()) { _player.TransitionTo(_player.SpellState); }
    }

    public override void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        _currentSpeed = 0;

        anim.SetBool("IsRunning", false);
    }
}
