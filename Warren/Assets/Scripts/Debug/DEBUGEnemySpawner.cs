using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUGEnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyStatBock> AllEnemies;
    [SerializeField] TMP_Dropdown Dropdown;

    int SelectedEnemy = 0;
    EnemyFactory EnemyFactory;

    private void Start()
    {
        List<string> sprites = new List<string>();
        foreach (var enemy in AllEnemies)
        {
            sprites.Add(enemy.name);
        }

        Dropdown.AddOptions(sprites);
        EnemyFactory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
    }

    public void OnValueChange(int id)
    {
        SelectedEnemy = id;
    }

    public void SpawnEnemy()
    {
        if (EnemyFactory == null) { print("No Enemy Factory in scene"); return; }

        EnemyFactory.SpawnEnemy(AllEnemies[SelectedEnemy], Vector3.zero);
    }
}
