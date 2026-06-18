using UnityEngine;

public class SM_Enemy : StateMachine
{
    // subclass of StateMachine that will be attached to the enemy.

    #region References
    public EnemyStats Stats {  get; private set; }
    public EntityCombat Combat { get; private set; }
    public EnemyInput Input { get; private set; }
    #endregion

    #region States
    public E_IdleState IdleState {  get; private set; }
    public E_FollowState FollowState { get; private set; }
    public E_AttackState AttackState { get; private set; }
    #endregion

    public void CreateStates()
    {
        Stats = GetComponent<EnemyStats>();
        Input = GetComponent<EnemyInput>();
        Combat = GetComponent<EntityCombat>();

        IdleState = new E_IdleState(this, Stats);
        FollowState = new E_FollowState(this, Stats);
        AttackState = new E_AttackState(this, Combat);
    }
}
