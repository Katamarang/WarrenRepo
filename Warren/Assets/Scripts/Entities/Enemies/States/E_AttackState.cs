using System.Collections.Generic;
using UnityEngine;

public class E_AttackState : IState
{
    // Attack state for the enemy.
    SM_Enemy _enemy;
    EnemyStats _stats;
    EntityCombat _combat;

    EnemyInput _input;
    Animator _animator;

    float attackCooldown;
    float _attackCooldownTime;

    WeaponCard Card;

    public E_AttackState(SM_Enemy enemy, EntityCombat stats)
    {
        _enemy = enemy;
        _combat = stats;

        _input = _enemy.Input;
        _animator = _stats.GetComponent<EntityStats>().Animator;
    }

    public override void Enter()
    {
        Card = _combat.PrimaryCard;
        attackCooldown = Card.BaseAttackCooldown + _combat.PrimaryCooldownModifier;

        _animator.SetTrigger("IsAttack");
        Card.OnFire(
            _combat.PrimaryDamageModifer, 
            _combat.PrimaryStatusEffects, 
            _combat.AttackPosition, 
            _combat.Damageable
            );
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
