using UnityEngine;

public class P_ParryInitState : IState
{
    // this is a blank script that will be duplicated when we want to create a new state

    SM_Player player;
    PlayerStats playerStats;

    public P_ParryInitState(SM_Player player, PlayerStats playerStats)
    {
        this.player = player;     
        this.playerStats = playerStats;
    }


    public void Enter()
    {
        //player.parry.parry();
    }

    public void Update()
    {
        player.TransitionTo(player.IdleState);
    }

    public void Exit()
    {

    }
}
