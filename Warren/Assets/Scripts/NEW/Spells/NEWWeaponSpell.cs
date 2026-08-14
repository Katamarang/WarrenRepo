using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NEWWeaponSpell : NEWSpell
{
    [Header("Weapon")]
    [SerializeField] float baseCooldown;  
    protected bool inCooldown;
    float finalCooldown;

    [SerializeField] float baseRadius;
    protected float finalRadius;

    [SerializeField] int baseDamage;
    protected int finalDamage;

    public void OnEnable()
    {
        NEWPlayerInput.OnAttackStarted += OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        NEWPlayerInput.OnAttackStarted -= OnAttackStarted;
        NEWPlayerInput.OnAttackCanceled -= OnAttackCanceled;
    }

    public virtual void OnAttackStarted(InputAction.CallbackContext context) 
    {
        finalRadius = NEWPlayerSpell.AdjustValue<IAttackRadius>(baseRadius, x => x.AdjustRadius());
        finalDamage = Mathf.RoundToInt(NEWPlayerSpell.AdjustValue<IAttackDamage>(baseDamage, x => x.AdjustDamage()));
    }

    public virtual void OnAttackCanceled(InputAction.CallbackContext context) { }

    protected void BeginCooldown()
    {
        inCooldown = true;

        finalCooldown = NEWPlayerSpell.AdjustValue<IAttackCooldown>(baseCooldown, x => x.AdjustCooldown());
    }

    private void Update()
    {
        if (!inCooldown) return;

        finalCooldown -= Time.deltaTime;
        if (finalCooldown < 0) inCooldown = false;
    }
}
