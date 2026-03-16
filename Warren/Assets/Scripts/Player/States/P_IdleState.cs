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

    public void Enter()
    {

    }

    public void Update()
    {
        if (_player.PlayerInput.ReadInput() != Vector2.zero)
        {
            _player.TransitionTo(new P_WalkState(_player, _playerStats));
        }

        if (_player.PlayerInput.Attack())
        {
            _player.TransitionTo(new P_AttackState(_player, _playerStats));
        }

        if (_player.PlayerInput.Parry())
        {
            _player.TransitionTo(new P_ParryState(_player, _playerStats));
        }
    }

    public void Exit()
    {

    }
}
