using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    // this class will be used to check for player proximity.
    // It will be used by the enemy's state machine to determine which state to transition to.

    EnemyStats stats;
    [SerializeField] float searchRange;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask Player;

    [SerializeField] bool playerInRange;
    [SerializeField] bool playerInAttackRange;

    public Vector2 FacingDirection {  get; private set; }

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    public bool PlayerInSearchRange()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, searchRange, Player);
        return playerInRange;
    }

    public bool PlayerInAttackRange()
    {
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, Player);
        return playerInAttackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
