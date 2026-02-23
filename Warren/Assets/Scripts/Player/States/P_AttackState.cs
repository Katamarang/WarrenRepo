using UnityEngine;

public class P_AttackState : IState
{
    SM_Player player;

    float attackCooldown;
    float comboCooldown;
    float _attackCooldownTime;
    float _comboCooldownTime;

    public P_AttackState(SM_Player player)
    {
        this.player = player;
        attackCooldown = player.Weapon.Values.AttackCooldown;
        comboCooldown = player.Weapon.Values.ComboCooldown;
    }

    public void Enter()
    {
        player.Weapon.OnAttack();
    }

    public void Update()
    {       
        if (_attackCooldownTime < attackCooldown) { _attackCooldownTime += Time.deltaTime; return; }

        if (_comboCooldownTime < comboCooldown)
        {
            if (player.PlayerInput.Attack())
            {
                player.TransitionTo(new P_AttackState(player));
            }

            _comboCooldownTime += Time.deltaTime;
        } else
        {
            player.TransitionTo(new P_IdleState(player));
        }                
    }

    public void Exit()
    {

    }
}
