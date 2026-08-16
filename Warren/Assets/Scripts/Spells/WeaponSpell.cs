using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public abstract class WeaponSpell : Spell
{
    [Header("Weapon")]
    [SerializeField] float attackLength;
    [SerializeField] float baseCooldown;  
    protected bool inCooldown;

    [SerializeField] float baseRadius;
    protected float finalRadius;

    [SerializeField] int baseDamage;
    protected int finalDamage;

    [Header("Animation")]
    [SerializeField] AnimationClip FrontAnim;
    [SerializeField] AnimationClip SideAnim;
    [SerializeField] AnimationClip BackAnim;

    PlayerMovement PlayerMovement;
    PlayerAnimator Animator;

    public void OnEnable()
    {
        PlayerInput.OnAttackStarted += OnAttackStarted;
        PlayerInput.OnAttackCanceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        PlayerInput.OnAttackStarted -= OnAttackStarted;
        PlayerInput.OnAttackCanceled -= OnAttackCanceled;
    }

    public override void OnInitialize(PlayerSpell spell)
    {
        base.OnInitialize(spell);
        PlayerMovement = PlayerSpell.GetComponent<PlayerMovement>();
        Animator = PlayerSpell.GetComponent<PlayerAnimator>();

        Animator.SetAnimation("Attack", FrontAnim, SideAnim, BackAnim);
    }

    public virtual void OnAttackStarted(InputAction.CallbackContext context) 
    {
        finalRadius = PlayerSpell.AdjustValue<IAdjustRadius>(baseRadius, x => x.AdjustRadius(), ModType.Weapon);
        finalDamage = Mathf.RoundToInt(PlayerSpell.AdjustValue<IAdjustDamage>(baseDamage, x => x.AdjustDamage(), ModType.Weapon));

        PlayerMovement.StopMovement();
        Animator.SetAnimTrigger("IsAttacking");
    }

    public virtual void OnAttackCanceled(InputAction.CallbackContext context) { }

    protected void OnHit(EntityHealth entity)
    {
        entity.TakeDamage(finalDamage);

        foreach (IApplyStatus status in PlayerSpell.GetSpellsOfType<IApplyStatus>(ModType.Weapon))
        {
            entity.ApplyStatusEffect(status.ApplyStatusEffect());
        }      
    }

    protected async Awaitable BeginCooldown()
    {
        inCooldown = true;
        float finalCooldown = PlayerSpell.AdjustValue<IAdjustCooldown>(baseCooldown, x => x.AdjustCooldown(), ModType.Weapon);

        await Awaitable.WaitForSecondsAsync(attackLength);

        PlayerMovement.StartMovement();

        await Awaitable.WaitForSecondsAsync(finalCooldown);

        inCooldown = false;
    }
}
