using Unity.VisualScripting;
using UnityEngine;

public class DamageModifierSpell : NEWModifierSpell, IAdjustDamage
{
    [Header("Modify Damage")]
    [SerializeField] int Damage;

    public int AdjustDamage()
    {
        return Damage;
    }
}
