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

    Transform attackPoint;
    float attackRadius;
    LayerMask player;

    int damage;

    public E_AttackState(SM_Enemy enemy, EnemyStats stats)
    {
        _enemy = enemy;
        _stats = stats;

        _input = _enemy.Input;
        _animator = _stats.Animator;

        attackPoint = _stats.AttackPoint;
        attackRadius = _stats.AttackRadius;
        player = _stats.Damageable;

        damage = _stats.MeleeDamage;
    }

    public override void Enter()
    {
        attackCooldown = _stats.MeleeCooldown;

        List<IDamageable> hit = _stats.MeleeBehaviour.OnFire(attackPoint, attackRadius, player);

        foreach (IDamageable hitItem in hit)
        {
            hitItem.TakeDamage(damage);
            if (_stats.MeleeDamageTypes.Count > 0 && hitItem is IStatus status)
            { status.ApplyStatusEffect(_stats.MeleeDamageTypes); }         
        }

        _animator.SetTrigger("IsAttack");
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
