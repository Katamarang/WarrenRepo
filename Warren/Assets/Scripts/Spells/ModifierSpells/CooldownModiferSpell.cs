using Unity.VisualScripting;
using UnityEngine;

public class CooldownModiferSpell : ModifierSpell, IAdjustCooldown
{
    [Header("Modify Cooldown")]
    [SerializeField] float Cooldown;

    public float AdjustCooldown()
    {
        return Cooldown;
    }
}
