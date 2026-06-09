using UnityEngine;

public class E_FollowState : IState
{
    // Follow state for the enemy. Will eveuntuall use pathfinding to follow the player.
    SM_Enemy _enemy;
    EnemyStats _stats;

    public E_FollowState(SM_Enemy enemy, EnemyStats stats)
    {
        _enemy = enemy;
        _stats = stats;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
