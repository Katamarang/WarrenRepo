using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitySpell : Spell
{
    // ability spells are those that require input from the player.

    [Header("Ability")]
    [SerializeField] protected ModType AbilityType;
    [SerializeField] float baseAbilityDuration;
    protected float finalAbilityDuration;
    [SerializeField] float baseAbilityCooldown;
    protected float finalAbilityCooldown;
    bool isAbilityActive;

    [Header("Animation")]
    [SerializeField] DirectionalAnim animationClips;
    [SerializeField] string animationSetName;
    [SerializeField] string triggerName;

    protected EntityInput EntityInput;
    PlayerMovement EntityMovement;
    
    PlayerAnimator Animator;

    public override void UpdateValues()
    {
        finalAbilityDuration = EntitySpell.AdjustValue(baseAbilityDuration, AbilityType, StatType.Duration);
        finalAbilityCooldown = EntitySpell.AdjustValue(baseAbilityCooldown, AbilityType, StatType.Cooldown);
    }

    public override void Initialised(EntitySpell spell)
    {     
        EntityMovement = spell.GetComponent<PlayerMovement>();
        EntityInput = spell.GetComponent<EntityInput>();

        Animator = spell.GetComponent<PlayerAnimator>();

        Animator.SetAnimation(animationSetName, animationClips);

        base.Initialised(spell);
    }

    public virtual void OnAbilityStarted()
    {
        EntityMovement.StopMovement();
        Animator.SetAnimTrigger(triggerName);
    }

    protected bool AbilityActive()
    {
        return isAbilityActive;
    }

    public virtual void OnAbilityEnded() { OnAbilityEnd(); }

    public virtual void OnAbilityEnd()
    {
        EntityMovement.StartMovement();
    }

    protected async Awaitable BeginCooldown()
    {
        isAbilityActive = true;

        await Awaitable.WaitForSecondsAsync(finalAbilityDuration);

        OnAbilityEnd();

        await Awaitable.WaitForSecondsAsync(finalAbilityCooldown);

        isAbilityActive = false;
    }
}
