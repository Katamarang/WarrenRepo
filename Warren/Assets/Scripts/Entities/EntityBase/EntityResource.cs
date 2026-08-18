using UnityEngine;

public class EntityResource : Entity
{
    [SerializeField] float MaxCharges;
    float SpellCharges;

    public float GetSpellCharges() { return SpellCharges; }

    public void AddSpellCharges(float spellCharges)
    {
        SpellCharges += spellCharges;
        SpellCharges = Mathf.Clamp(SpellCharges, 0, MaxCharges);
    }
    
    public void RemoveSpellCharges(float spellCharges)
    {
        SpellCharges -= spellCharges;
        SpellCharges = Mathf.Clamp(SpellCharges, 0, MaxCharges);
    }
}
