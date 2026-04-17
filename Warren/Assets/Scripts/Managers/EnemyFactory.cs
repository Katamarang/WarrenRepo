using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject BaseEnemy;

    [Header("DEBUG")]
    public EnemyStatBock Enemy;

    public void SpawnEnemy(EnemyStatBock statBlock, Vector3 transform)
    {
        GameObject enemy = Instantiate(BaseEnemy, transform, Quaternion.identity);
        enemy.name = statBlock.name;
        enemy.GetComponent<EnemyStats>().OnInstantate(statBlock);
    }

    [ContextMenu("Spawn Enemy DEBUG")]
    public void SpawnEnemyDEBUG()
    {
        SpawnEnemy(Enemy, Vector3.zero);
    }
}
