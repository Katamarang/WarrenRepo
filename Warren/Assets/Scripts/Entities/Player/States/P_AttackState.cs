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

    int damage;

    Animator _anim;

    public P_AttackState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

        _anim = _player.Animator;       
    }

    public override void Enter()
    {
        UpdateStats();

        _player.Animator.SetTrigger("IsAttacking");
        List<IDamageable> hit = _playerStats.MeleeBehaviour.OnFire(attackPoint, attackRadius, enemy);

        foreach (IDamageable hitItem in hit)
        {
            hitItem.TakeDamage(damage);
            if (_playerStats.MeleeDamageTypes.Count > 0 && hitItem is IStatus status) 
            { status.ApplyStatusEffect(_playerStats.MeleeDamageTypes); } 
        }
        //if (hit.Count != 0) Debug.Log(hit[0]);
    }

    public override void Update()
    {       
        if (_attackCooldownTime < attackCooldown) { _attackCooldownTime += Time.deltaTime; return; }

       // combo code goes here 
        _player.TransitionTo(_player.IdleState);                      
    }

    public override void Exit()
    {
        _attackCooldownTime = 0;
    }

    private void UpdateStats()
    {
        attackCooldown = _playerStats.MeleeCooldown;

        attackPoint = _playerStats.AttackPoint;
        attackRadius = _playerStats.AttackRadius;
        enemy = _playerStats.Damageable;

        damage = _playerStats.MeleeDamage;
    }

    public override void FixedUpdate() { }
}
