using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Modifier", menuName = "Scriptable Objects/Cards/Modifier Card")]
public class ModifierCard : PlayerCard
{
    // creatable object for modifier cards, contains all the information about the modifier.
    [Header("Modifier")]
    public ModType ModType;
    public Mod[] StatModifier;
}

public enum Stat // enum for the stats that can be modified by the modifier card.
{
    Damage,
    Speed,
    Acceleration,
    Range,

    SpellCost,
    ElementType,
    StatusResistant,
    StatusVunerable,

    MaxHealth,
    CurrentHealth    
}

public enum ModType { Weapon, Spell, Entity, World } // enum what what the modifier card modifies.

[Serializable]
public class Mod
{
    public Stat Stat;
    public float Modifier;
}
