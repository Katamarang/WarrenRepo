using UnityEngine;
using System.Collections.Generic;

public class P_AttackState : IState
{
    // Player's attack state
    SM_Player _player;
    PlayerCombat _playerCombat;

    float attackCooldown;
    float _attackCooldownTime;

    WeaponCard card;

    public P_AttackState(SM_Player player, PlayerCombat playerCombat)
    {
        _player = player;
        _playerCombat = playerCombat;

    }

    public override void Enter()
    {
        card = _playerCombat.PrimaryCard;
        if (card == null) { return; }
        attackCooldown = card.BaseAttackCooldown + _playerCombat.PrimaryCooldownModifier;

        _player.Animator.SetTrigger("IsAttacking");

        _playerCombat.PrimaryCard?.OnFire
            (
                _playerCombat.PrimaryDamageModifer,
                _playerCombat.PrimaryStatusEffects,
                _playerCombat.AttackPosition,
                _playerCombat.Damageable
            ); // fires the weapon
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
