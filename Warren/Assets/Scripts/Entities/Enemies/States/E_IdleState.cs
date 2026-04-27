using UnityEngine;

public class E_IdleState : IState
{
    SM_Enemy _enemy;
    EnemyStats _stats;

    EnemyInput _input;

    Animator _animator;

    public E_IdleState(SM_Enemy enemy, EnemyStats stats)
    {
        _enemy = enemy;
        _stats = stats;

        _input = _enemy.Input;
        _animator = _stats.Animator;
    }

    public override void Enter()
    {
        _animator.SetBool("IsMoving", false);
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (_input.PlayerInAttackRange())
        {
            _enemy.TransitionTo(_enemy.AttackState);
        } else if (_input.PlayerInSearchRange()) { }
    }
}
