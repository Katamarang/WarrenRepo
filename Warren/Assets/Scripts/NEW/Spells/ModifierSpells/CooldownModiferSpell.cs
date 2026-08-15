using Unity.VisualScripting;
using UnityEngine;

public class CooldownModiferSpell : NEWModifierSpell, IAdjustCooldown
{
    [Header("Modify Cooldown")]
    [SerializeField] float Cooldown;

    public float AdjustCooldown()
    {
        return Cooldown;
    }
}
