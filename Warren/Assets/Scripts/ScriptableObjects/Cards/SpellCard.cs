using UnityEngine;

[CreateAssetMenu(fileName = "new Spell", menuName = "Scriptable Objects/Cards/Player Cards/Spell Card")]
public class SpellCard : PlayerCard
{
    [Header("Spell")]
    public int BaseDamage;
    public float BaseRadius;
    public int BaseCost;
    public float SpellLength;

    public WeaponBehaviour Behaviour;
    public AnimationClip Animation;
}
