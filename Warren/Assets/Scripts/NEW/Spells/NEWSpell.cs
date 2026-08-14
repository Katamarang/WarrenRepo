using UnityEngine;

public abstract class NEWSpell : MonoBehaviour
{
    [Header("Spell")]
    public string Name;

    [TextArea(3, 5)] 
    public string Desc;

    [Space(10)]
    public Sprite IconSprite;

    public virtual void OnInitialize() { }
}

public interface IAttackCooldown { public float AdjustCooldown() { return 0; } }
public interface IAttackRadius { public float AdjustRadius() {  return 0; }  }
public interface IAttackDamage { public int AdjustDamage() {  return 0; }  }
