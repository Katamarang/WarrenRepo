using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Mod", menuName = "Scriptable Objects/Cards/Player Cards/Weapon Mod Card")]
public class WeaponModCard : PlayerCard
{
    [Header("Weapon Modifier")]
    public WeaponMod[] WeaponModifiers;
}

public enum WeaponStat
{
    Damage,
    Speed,
    DamageType
}

[Serializable]
public class WeaponMod
{
    public WeaponStat WeaponStat;
    public float Modifier;
}
