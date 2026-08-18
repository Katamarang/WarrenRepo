using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Abilities/Spells/Base Spell")]
public class SpellSpell : AbilitySpell
{
    EntityResource resources;

    [SerializeField] float SpellCost;

    public override void Initialised(EntitySpell spell)
    {
        resources = spell.GetComponent<EntityResource>();
        base.Initialised(spell);
    }

    public override void OnEnabled()
    {
        base.OnEnabled();
        EntityInput.OnSpellPressed += OnAbilityStarted;
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
        EntityInput.OnSpellPressed -= OnAbilityStarted;
    }

    public override async void OnAbilityStarted()
    {
        if (AbilityActive()) return;
        if (resources.GetSpellCharges() < SpellCost) return;

        base.OnAbilityStarted();

        resources.RemoveSpellCharges(SpellCost);
        await BeginCooldown();
    }
}
