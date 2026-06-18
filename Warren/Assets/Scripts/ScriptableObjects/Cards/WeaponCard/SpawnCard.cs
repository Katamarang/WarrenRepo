using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnCard", menuName = "Scriptable Objects/Cards/Weapons/Spawner")]
public class SpawnCard : WeaponCard
{
    [Header("Spawn Card")]
    public GameObject ToSpawn;

    public override void OnFire(int damageMod, List<StatusEffect> statusEffects, Transform pos, LayerMask target)
    {
        MonoBehaviour.Instantiate(ToSpawn, pos.position, pos.localRotation);
    }
}
