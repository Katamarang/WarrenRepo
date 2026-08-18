using UnityEngine;

[CreateAssetMenu(fileName = "New Apply Status", menuName = "Spells/Modifiers/Apply Status Effect")]
public class ApplyStatusSpell : ModifierSpell, IApplyStatus
{
    [Header("Status Effect")]
    [SerializeField] DamageType Status;

    public DamageType ApplyStatusEffect()
    {
       return Status;
    }
}
