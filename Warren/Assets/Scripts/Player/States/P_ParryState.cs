using UnityEngine;

public class P_ParryState : IState
{

    SM_Player _player;
    PlayerStats _playerStats;

    float parryWindow;
    float _parryWindowTime;

    public P_ParryState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;     
        _playerStats = playerStats;

        parryWindow = _playerStats.ParryWindow;
    }


    public void Enter()
    {
        _player.Animator.SetTrigger("IsParrying");
    }

    public void Parry()
    {
        _playerStats.SpellCharges++;
    }

    public void Update()
    {
        if (_parryWindowTime < parryWindow) { _parryWindowTime += Time.deltaTime; return; }

        _player.TransitionTo(new P_IdleState(_player, _playerStats));
    }

    public void Exit()
    {
        _parryWindowTime = 0;
    }
}
