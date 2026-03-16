using UnityEngine;

public class P_SpellState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;


    public P_SpellState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;
    }

    public void Enter()
    {
        Debug.Log("Spell");
    }

    public void Update()
    {
        _player.TransitionTo(new P_IdleState(_player, _playerStats));
    }

    public void Exit()
    {

    }
}
