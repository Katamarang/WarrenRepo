using UnityEngine;

public class Card : ScriptableObject
{
    // base class for all cards.
    public string CardName;
    
    [TextArea(3, 5)]public string CardDescription;

    [Space(10)]
    public Sprite CardSprite;
}
