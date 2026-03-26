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
    }


    public override void Enter()
    {
        parryWindow = _playerStats.ParryWindow;

        _player.Animator.SetTrigger("IsParrying");
        Parry(); // TEMP
    }

    public void Parry() // only gets called when Parry is successful
    {
        _player.SpellState.AddSpellCharge();
    }

    public override void Update()
    {
        if (_parryWindowTime < parryWindow) { _parryWindowTime += Time.deltaTime; return; }

        _player.TransitionTo(_player.IdleState);
    }

    public override void Exit()
    {
        _parryWindowTime = 0;
    }

    public override void FixedUpdate() { }
}
