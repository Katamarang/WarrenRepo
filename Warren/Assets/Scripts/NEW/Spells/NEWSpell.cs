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
