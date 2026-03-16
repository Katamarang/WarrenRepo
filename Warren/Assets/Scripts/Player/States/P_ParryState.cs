using UnityEngine;

public class P_ParryState : IState
{
    // this is a blank script that will be duplicated when we want to create a new state

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
