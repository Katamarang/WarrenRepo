using UnityEngine;
using System.Collections.Generic;

public class P_AttackState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    float attackCooldown;
    float comboCooldown;

    Transform attackPoint;
    float attackRadius;
    LayerMask enemy;

    float _attackCooldownTime;
    float _comboCooldownTime;

    int damage;

    Animator _anim;

    public P_AttackState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

        _anim = _player.Animator;
        
        attackCooldown = playerStats.MeleeCooldown;
        comboCooldown = playerStats.ComboCooldown;

        attackPoint = playerStats.AttackPoint;
        attackRadius = playerStats.AttackRadius;
        enemy = playerStats.Damageable;

        damage = playerStats.MeleeDamage;
    }

    public void Enter()
    {
        _player.Animator.SetTrigger("IsAttacking");
        List<IDamageable> hit = _playerStats.MeleeBehaviour.OnFire(attackPoint, attackRadius, enemy);

        foreach (IDamageable hitItem in hit)
        {
            hitItem.OnDamage(damage);
        }
    }

    public void Update()
    {       
        if (_attackCooldownTime < attackCooldown) { _attackCooldownTime += Time.deltaTime; return; }

        if (_comboCooldownTime < comboCooldown)
        {
            if (_player.PlayerInput.Attack())
            {
                _player.TransitionTo(_player.AttackState);
            }

            _comboCooldownTime += Time.deltaTime;
        } else
        {
            _player.TransitionTo(_player.IdleState);
        }                
    }

    public void Exit()
    {
        _attackCooldownTime = 0;
        _comboCooldownTime = 0;
    }
}
