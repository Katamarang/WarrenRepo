using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Modifier", menuName = "Scriptable Objects/Cards/Player Cards/Modifier Card")]
public class ModifierCard : PlayerCard
{
    [Header("Modifier")]
    public ModType ModType;
    public Mod[] StatModifier;
}

public enum Stat
{
    Damage,
    Speed,
    Cost,
    DamageType,
    Range,
    CurrentHealth,
    MaxHealth,
    Acceleration
}

public enum ModType { Weapon, Spell, Player, World }

[Serializable]
public class Mod
{
    public Stat Stat;
    public float Modifier;
}
