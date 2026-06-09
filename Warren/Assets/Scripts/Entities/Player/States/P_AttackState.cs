using UnityEngine;
using System.Collections.Generic;

public class P_AttackState : IState
{
    // Player's attack state
    SM_Player _player;
    PlayerStats _playerStats;

    float attackCooldown;

    float _attackCooldownTime;

    public P_AttackState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

    }

    public override void Enter()
    {
        attackCooldown = _playerStats.MeleeCooldown;

        _player.Animator.SetTrigger("IsAttacking");

        _playerStats.WeaponBehaviour?.OnFire(); // fires the weapon
    }

    public override void Update()
    {
        // will transition back to idle once the attack cooldown ends.
        if (_attackCooldownTime < attackCooldown) { _attackCooldownTime += Time.deltaTime; return; } 

        _player.TransitionTo(_player.IdleState);                      
    }

    public override void Exit()
    {
        _attackCooldownTime = 0;
    }

    public override void FixedUpdate() { }
}
