using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Scriptable Objects/Enemy Stat Bock")]
public class EnemyStatBock : ScriptableObject
{
    // Scriptable Object that holds all the stats for an enemy
    [Header("Base Stats")]
    public int BaseHealth;
    public float BaseSpeed;

    [Header("Cards")]
    public WeaponSpell WeaponCard;
    public int ModCardAmount;
    public List<ModifierSpell> CardPool;

    [Space(20)]
    public AnimatorOverrideController AnimatorOverride;
}
