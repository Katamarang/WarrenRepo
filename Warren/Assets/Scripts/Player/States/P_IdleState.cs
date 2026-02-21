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
        if (_player.InputDirection != Vector2.zero)
        {
            _player.TransitionTo(new P_WalkState(_player));
        }
    }

    public void Exit()
    {

    }
}
