using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("Spell")]
    public string Name;

    [TextArea(3, 5)] 
    public string Desc;

    [Space(10)]
    public Sprite IconSprite;

    protected PlayerSpell PlayerSpell;

    public virtual void OnInitialize(PlayerSpell spell) { PlayerSpell = spell; }
}

public interface IAdjustCooldown { public float AdjustCooldown(); }
public interface IAdjustRadius { public float AdjustRadius();  }
public interface IAdjustDamage { public int AdjustDamage();  }
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
    Spell,
    World
}

