using UnityEngine;

public class P_AttackState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    float attackCooldown;
    float comboCooldown;
    float _attackCooldownTime;
    float _comboCooldownTime;

    Animator _anim;

    public P_AttackState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;

        _anim = _player.Animator;
        
        attackCooldown = playerStats.MeleeCooldown;
        comboCooldown = playerStats.ComboCooldown;
    }

    public void Enter()
    {
        _player.Animator.SetTrigger("IsAttacking");
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
