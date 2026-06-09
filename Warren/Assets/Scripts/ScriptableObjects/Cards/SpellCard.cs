using UnityEngine;

[CreateAssetMenu(fileName = "new Spell", menuName = "Scriptable Objects/Cards/Spell Card")]
public class SpellCard : PlayerCard
{
    // creatable object for spell cards, contains all the information about the spell.
    [Header("Spell")]
    public int BaseDamage;
    public float BaseRadius;
    public int BaseCost;
    public float SpellLength;
    public WeaponBehaviours behaviour;

    public AnimationClip Animation;
}
