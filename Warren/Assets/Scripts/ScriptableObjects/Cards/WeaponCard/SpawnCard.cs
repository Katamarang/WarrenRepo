using UnityEngine;

[CreateAssetMenu(fileName = "SpawnCard", menuName = "Scriptable Objects/Cards/Weapons/Spawner")]
public class SpawnCard : WeaponCard // class used for organisation. Holds the prefab to be spawned.
{
    [Header("Spawn Card")]
    public GameObject ToSpawn;
}

public class SpawnBehaviour : WeaponBehaviour
{
    public GameObject ToSpawn;


    public override void OnFire() // will spawn the prefab at the position of the entity.
    {
        MonoBehaviour.Instantiate(ToSpawn, pos.position, pos.localRotation);
    }
}
