using UnityEngine;

public abstract class GenAlgorithm : MonoBehaviour
{
    public abstract int[,] StartGeneration(BoundsInt WorldSize, int seed);

    internal Vector2Int[] neighbours = new Vector2Int[]
    {
        new(1,0),
        new(1,1),
        new(0,1),
        new(-1,1),
        new(-1,0),
        new(-1,-1),
        new(0,-1),
        new(1,-1)
    };

    internal BoundsInt bounds;
}
