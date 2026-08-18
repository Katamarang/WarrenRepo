using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Value Modifier", menuName = "Spells/Modifiers/Value")]
public class ValueModiferSpell : ModifierSpell, IAdjustValue
{
    [Header("Modify Cooldown")]
    [Tooltip("Less that one decreases value, more than one increases value"), SerializeField, Range(0,2)] 
    float Multiplier = 1;

    public float AdjustValue(float initialValue)
    {
        return initialValue * Multiplier;
    }
}
