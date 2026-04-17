using UnityEngine;

public class SM_Enemy : StateMachine
{
    #region References
    public EnemyStats Stats {  get; private set; }
    public EnemyInput Input { get; private set; }
    #endregion

    #region States
    public E_IdleState IdleState {  get; private set; }
    public E_FollowState FollowState { get; private set; }
    //public E_AttackState AttackState { get; private set; }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<EnemyStats>();
        Input = GetComponent<EnemyInput>();
    }

    private void CreateStates()
    {
        IdleState = new E_IdleState(this, Stats);
        FollowState = new E_FollowState(this, Stats);
    }
}
