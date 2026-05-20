using UnityEngine;

[CreateAssetMenu(fileName = "SpawnCard", menuName = "Scriptable Objects/Cards/Weapons/Spawner")]
public class SpawnCard : WeaponCard
{
    [Header("Spawn Card")]
    public GameObject ToSpawn;
}

public class SpawnBehaviour : WeaponBehaviour
{
    public GameObject ToSpawn;


    public override void OnFire()
    {
        MonoBehaviour.Instantiate(ToSpawn, pos.position, pos.localRotation);
    }
}
