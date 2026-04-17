using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Scriptable Objects/Enemy Stat Bock")]
public class EnemyStatBock : ScriptableObject
{
    [Header("Base Stats")]
    public int BaseHealth;
    public float BaseSpeed;

    [Header("Cards")]
    public WeaponCard WeaponCard;
    public int ModCardAmount;
    public List<ModifierCard> CardPool;

    [Space(20)]
    public AnimatorOverrideController AnimatorOverride;
}
