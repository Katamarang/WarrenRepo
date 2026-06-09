using UnityEngine;

public class P_IdleState : IState
{
    // Player's idle state

    SM_Player _player;
    PlayerStats _playerStats;
    PlayerInput _playerInput;

    public P_IdleState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

        _playerInput = _player.PlayerInput;
    }

    public override void Enter() { }

    public override void Update() // checks for input to transition to other states.
    {
        if (_playerInput.ReadInput() != Vector2.zero) { _player.TransitionTo(_player.WalkState); }
        else if (_playerInput.Attack()) { _player.TransitionTo(_player.AttackState); }
        else if (_playerInput.Parry()) { _player.TransitionTo(_player.ParryState); }
        else if (_playerInput.Spell()) { _player.TransitionTo(_player.SpellState); }
    }

    public override void Exit() { }

    public override void FixedUpdate() { }
}
