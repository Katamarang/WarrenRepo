using UnityEngine;

public class ApplyStatusSpell : ModifierSpell, IApplyStatus
{
    [Header("Status Effect")]
    [SerializeField] DamageType Status;

    public DamageType ApplyStatusEffect()
    {
       return Status;
    }
}
