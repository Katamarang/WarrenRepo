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
            _player.TransitionTo(_player.WalkState);
        }

        if (_player.PlayerInput.Attack())
        {
            _player.TransitionTo(_player.AttackState);
        }

        if (_player.PlayerInput.Parry())
        {
            _player.TransitionTo(_player.ParryStartState);
        }
    }

    public void Exit()
    {

    }
}
