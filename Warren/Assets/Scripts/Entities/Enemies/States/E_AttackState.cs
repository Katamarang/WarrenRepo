using System.Collections.Generic;
using UnityEngine;

public class E_AttackState : IState
{
    SM_Enemy _enemy;
    EnemyStats _stats;

    EnemyInput _input;
    Animator _animator;

    float attackCooldown;
    float _attackCooldownTime;

    public E_AttackState(SM_Enemy enemy, EnemyStats stats)
    {
        _enemy = enemy;
        _stats = stats;

        _input = _enemy.Input;
        _animator = _stats.Animator;
    }

    public override void Enter()
    {
        attackCooldown = _stats.MeleeCooldown;

        _animator.SetTrigger("IsAttack");
        _stats.WeaponBehaviour.OnFire();
    }

    public override void Exit()
    {
        _attackCooldownTime = 0;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (_attackCooldownTime < attackCooldown) { _attackCooldownTime += Time.deltaTime; return; }

        _enemy.TransitionTo(_enemy.IdleState);
    }
}
