using UnityEngine;
using System.Collections.Generic;

public class EntityStats : Entity
{
    // class for controlling the stats of an entity.

    internal CardLoader cardLoader;
    internal List<Card> cards = new List<Card>();

    [Header("Movement")]
    public float MaxSpeed;
    public float SpeedModifier;
    public float Acceleration;

    [Space(15)]
    public Animator Animator;
}
