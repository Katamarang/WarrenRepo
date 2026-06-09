using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUGEnemySpawner : MonoBehaviour
{
    // A debug script for spawning enemies using the Enemy Factory. requires an Enemy Factory in the scene to work.

    [SerializeField] List<EnemyStatBock> AllEnemies;
    [SerializeField] TMP_Dropdown Dropdown;

    int SelectedEnemy = 0;
    EnemyFactory EnemyFactory;
    bool AIoff = true;

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

    public void TurnOffAI(bool AI) // Called by the toggle when the value is changed, sets whether the spawned enemy will have AI or not.
    {
        AIoff = AI;
    }

    public void OnValueChange(int id) // Called by the dropdown when the value is changed, sets the selected enemy to spawn.
    {
        SelectedEnemy = id;
    }

    public void SpawnEnemy() // tells the Enemy Factory to spawn the selected enemy at 0,0 with or without AI.
    {
        if (EnemyFactory == null) { print("No Enemy Factory in scene"); return; }

        EnemyFactory.SpawnEnemy(AllEnemies[SelectedEnemy], Vector3.zero, AIoff);
    }
}
