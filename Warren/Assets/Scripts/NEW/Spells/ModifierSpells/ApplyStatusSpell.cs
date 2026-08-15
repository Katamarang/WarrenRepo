using UnityEngine;

public class ApplyStatusSpell : NEWModifierSpell, IApplyStatus
{
    [Header("Status Effect")]
    [SerializeField] StatusEffect Status;

    public StatusEffect ApplyStatusEffect()
    {
       return Status;
    }
}
