using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new Stat Mod", menuName = "Scriptable Objects/Cards/Player Cards/Stat Mod Card")]
public class StatModCard : PlayerCard
{
    [Header("Player Stats")]
    public ModifyStats[] StatsToModify;
}

public enum PlayerGameStats
{
    CurrentHealth,
    MaxHealth,
    Speed,
    Acceleration,
    Resistance
}

[Serializable]
public class ModifyStats
{
    public PlayerGameStats Stat;
    public int Modifier;
}
