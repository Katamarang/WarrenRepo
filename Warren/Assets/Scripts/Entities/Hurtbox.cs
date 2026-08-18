using UnityEngine;
using UltEvents;
using System.Collections.Generic;

public class Hurtbox : MonoBehaviour, IVariableValues
{
    [SerializeField] UltEvent<int, List<IApplyStatus>> OnHitEvent;
    ParrySpell canParry;
    EntitySpell spell;

    private void Awake() { spell = GetComponentInParent<EntitySpell>(); }

    private void OnEnable() { spell.UpdateValues += UpdateValues; }

    private void OnDisable() { spell.UpdateValues -= UpdateValues; }

    public void OnHit(int damage, List<IApplyStatus> statuseffects)
    {
        if (canParry && canParry.IsParrying)
        {
            canParry.Parry();
            return;
        }
        
        OnHitEvent?.Invoke(damage, statuseffects);
    }

    public void UpdateValues()
    {
        spell.ContainsSpell<ParrySpell>(out canParry);
    }
}
