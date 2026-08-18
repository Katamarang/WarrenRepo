using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Parry", menuName = "Spells/Abilities/Parries/Base Parry")]
public class ParrySpell : AbilitySpell
{
    EntityResource resources;

    [Header("Parry")]
    public bool IsParrying;

    public override void Initialised(EntitySpell spell)
    {
        resources = spell.GetComponent<EntityResource>();

        base.Initialised(spell);
    }

    public override void OnEnabled() 
    { 
        base.OnEnabled();
        EntityInput.OnParryPressed += OnAbilityStarted; 
    }

    public override void OnDisabled() 
    { 
        base.OnDisabled();
        EntityInput.OnParryPressed -= OnAbilityStarted; 
    }

    public override async void OnAbilityStarted()
    {
        if (AbilityActive()) return;

        base.OnAbilityStarted();

        IsParrying = true;
        Parry(); //TEMPORARY

        await BeginCooldown();
    }

    public override void OnAbilityEnd()
    {
        base.OnAbilityEnd();

        IsParrying = false;
    }

    public virtual void Parry()
    {
        resources.AddSpellCharges(1);
    }

}
