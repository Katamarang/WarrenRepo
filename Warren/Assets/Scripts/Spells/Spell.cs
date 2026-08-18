using UnityEngine;

public abstract class Spell : ScriptableObject, IVariableValues
{
    [Header("Spell")]
    public string Name;

    [TextArea(3, 5)] 
    public string Desc;

    [Space(10)]
    public Sprite IconSprite;

    protected EntitySpell EntitySpell;

    public virtual void Initialised(EntitySpell spell) 
    { 
        EntitySpell = spell;

        OnEnabled();
    }

    public virtual void OnEnabled() { EntitySpell.UpdateValues += UpdateValues; }

    public virtual void OnDisabled() { EntitySpell.UpdateValues -= UpdateValues; }

    public virtual void UpdateValues() { }
}

public interface IAdjustValue { public float AdjustValue(float initialValue); }
public interface IApplyStatus { public DamageType ApplyStatusEffect(); }

public enum DamageType
{
    None,
    Fire,
    Lightning,
    Poison
}

public enum ModType
{
    None,
    Weapon,
    Parry,
    Spell,
    Player,
    World
}

public enum StatType
{
    Duration,
    Cooldown,
    Radius,
    Speed,
    Damage,
    Status
}
