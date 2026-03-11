using UnityEngine;

public class Card : ScriptableObject
{
    public string CardName;
    
    [TextArea(3, 5)]public string CardDescription;

    [Space(10)]
    public Sprite CardSprite;
}
