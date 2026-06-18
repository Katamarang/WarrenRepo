using UnityEngine;

public class P_ParryState : IState
{
    // Player's parry state

    SM_Player _player;
    PlayerCombat _playerCombat;

    float parryWindow;
    float _parryWindowTime;

    public P_ParryState(SM_Player player, PlayerCombat combat)
    {
        _player = player;     
        _playerCombat = combat;      
    }


    public override void Enter()
    {
        parryWindow = _playerCombat.ParryWindow;

        _player.Animator.SetTrigger("IsParrying");
        Parry(); // TEMP. Will be called if the player takes damage during the parry window.
    }

    public void Parry() // only gets called when Parry is successful
    {
        _playerCombat.ManaCharges++;
    }

    public override void Update()
    {
        if (_parryWindowTime < parryWindow) { _parryWindowTime += Time.deltaTime; return; }

        _player.TransitionTo(_player.IdleState); // will transition back to idle if the parry window ends.
    }

    public override void Exit()
    {
        _parryWindowTime = 0;
    }

    public override void FixedUpdate() { }
}
