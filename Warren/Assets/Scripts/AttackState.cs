using UnityEngine;

public class AttackState : IState
{
    StateMachine entity;
    bool attackOver;

    public AttackState(StateMachine entity)
    {
        this.entity = entity;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        SM_Player _player = entity as SM_Player;
        if (_player != null)
        {
            _player.TransitionTo(new P_IdleState(_player));
        }
    }

    public void Exit()
    {

    }
}
