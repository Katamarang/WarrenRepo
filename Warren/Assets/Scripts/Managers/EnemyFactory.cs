using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject BaseEnemy;

    [Header("DEBUG")]
    public EnemyStatBock Enemy;

    public void SpawnEnemy(EnemyStatBock statBlock, Vector3 transform, bool AIOff)
    {
        GameObject enemy = Instantiate(BaseEnemy, transform, Quaternion.identity);
        enemy.name = statBlock.name;
        enemy.GetComponent<EnemyStats>().OnInstantate(statBlock);

        if (AIOff){ enemy.GetComponent<SM_Enemy>().enabled = false; }
    }

    [ContextMenu("Spawn Enemy DEBUG")]
    public void SpawnEnemyDEBUG()
    {
        SpawnEnemy(Enemy, Vector3.zero, false);
    }
}
