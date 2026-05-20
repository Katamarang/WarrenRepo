using UnityEngine;
using System.Collections.Generic;

public class P_AttackState : IState
{
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
        UpdateStats();

        _player.Animator.SetTrigger("IsAttacking");

        _playerStats.WeaponBehaviour?.OnFire(); // fires the weapon
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
    }

    public override void FixedUpdate() { }
}
