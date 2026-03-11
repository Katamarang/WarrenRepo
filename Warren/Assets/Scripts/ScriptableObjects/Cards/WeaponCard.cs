using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Cards/Player Cards/Weapon Card")]
public class WeaponCard : PlayerCard
{
    [Header("Weapon")]
    public Sprite WeaponSprite;

    public int BaseDamage;

    public float BaseAttackCooldown;
    public WeaponType WeaponType;

    public WeaponBehaviour Behaviour;

}

public enum DamageType
{
    None,
    Fire, // Damage over time
    Poison, // Slows
    Lightning // high damage, has a few seconds charge up
}

public enum WeaponType
{
    Attack,
    Parry,
    Dash
}
