using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    EnemyStats stats;
    [SerializeField] float searchRange;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask Player;


    public Vector2 FacingDirection {  get; private set; }

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    public bool PlayerInSearchRange()
    {
        return Physics2D.OverlapCircle(transform.position, searchRange, Player);
    }

    public bool PlayerInAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange, Player);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
