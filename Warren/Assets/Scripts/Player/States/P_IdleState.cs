using UnityEngine;

public class P_IdleState : IState
{
    SM_Player _player;

    public P_IdleState(SM_Player player)
    {
       _player = player;
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
    }

    public void Exit()
    {

    }
}
