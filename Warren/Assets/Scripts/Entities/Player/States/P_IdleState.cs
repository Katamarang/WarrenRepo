using UnityEngine;

public class P_IdleState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    public P_IdleState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;
    }

    public override void Enter() { }

    public override void Update()
    {
        if (_player.PlayerInput.ReadInput() != Vector2.zero) { _player.TransitionTo(_player.WalkState); }
        else if (_player.PlayerInput.Attack()) { _player.TransitionTo(_player.AttackState); }
        else if (_player.PlayerInput.Parry()) { _player.TransitionTo(_player.ParryState); }
        else if (_player.PlayerInput.Spell()) { _player.TransitionTo(_player.SpellState); }
    }

    public override void Exit() { }

    public override void FixedUpdate() { }
}
