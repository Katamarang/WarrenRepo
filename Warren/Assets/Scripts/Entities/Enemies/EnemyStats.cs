using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats
{
    public int MaxHealth = 50;

    public float MaxSpeed = 6;

    public int Damage = 5;

    private void Start()
    {
        GetComponent<EntityHealth>().Load();
    }

    public void LoadCardLoader()
    {
        //throw new System.NotImplementedException();
    }
}
