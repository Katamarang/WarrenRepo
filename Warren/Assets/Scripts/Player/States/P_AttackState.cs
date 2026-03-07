using UnityEngine;

public class P_AttackState : IState
{
    SM_Player _player;

    float attackCooldown;
    float comboCooldown;
    float _attackCooldownTime;
    float _comboCooldownTime;

    public P_AttackState(SM_Player player)
    {
        _player = player;
        attackCooldown = player.Weapon.Values.AttackCooldown;
        comboCooldown = player.Weapon.Values.ComboCooldown;
    }

    public void Enter()
    {
        _player.Weapon.OnAttack();
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
