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

    PlayerMovement PlayerMovement;
    PlayerAnimator Animator;

    public override void UpdateValues()
    {
        finalAbilityDuration = PlayerSpell.AdjustValue<IAdjustCooldown>(baseAbilityDuration, x => x.AdjustCooldown(), AbilityType);
        finalAbilityCooldown = PlayerSpell.AdjustValue<IAdjustCooldown>(baseAbilityCooldown, x => x.AdjustCooldown(), AbilityType);
    }

    public override void Initialised(PlayerSpell spell)
    {
        base.Initialised(spell);
        PlayerMovement = PlayerSpell.GetComponent<PlayerMovement>();
        Animator = PlayerSpell.GetComponent<PlayerAnimator>();

        Animator.SetAnimation(animationSetName, animationClips);        
    }

    public virtual void OnAbilityStarted(InputAction.CallbackContext context)
    {
        if (isAbilityActive) return;

        PlayerMovement.StopMovement();
        Animator.SetAnimTrigger(triggerName);
    }

    public virtual void OnAbilityEnded(InputAction.CallbackContext context) { OnAbilityEnd(); }

    public virtual void OnAbilityEnd()
    {
        PlayerMovement.StartMovement();
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
