using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Cards/Weapon Card")]
public class WeaponCard : PlayerCard
{
    [Header("Weapon")]
    public Sprite WeaponSprite;

    public int BaseDamage;
    public float BaseAttackRadius;

    public float BaseAttackCooldown;

    public WeaponBehaviour Behaviour;

}

public enum DamageType
{
    None,
    Fire, // Damage over time
    Poison, // Slows
    Lightning // high damage, has a few seconds charge up
}
