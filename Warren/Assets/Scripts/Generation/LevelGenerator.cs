using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    // per level script

    public int[,] WORLDGRID { get; private set; }

    public int SEED;
    [SerializeField] bool RandomSeed;

    [Header("Tilemap")]

    [SerializeField] Tilemap Tilemap;
    [SerializeField] BoundsInt WorldSize;

    [SerializeField] GenAlgorithm Biome;

    [SerializeField] Tile tile;

    public static Action<int[,]> GenerateMap;
    public static Action<int[,]> PaintMap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GenerateLevel();
        }
    }

    [ContextMenu("Generate Level")]
    public void GenerateLevel()
    {
        Tilemap.ClearAllTiles();
        WORLDGRID = new int[WorldSize.size.x, WorldSize.size.y];

        SEED = RandomSeed? UnityEngine.Random.Range(1,999) : SEED; //generates a seed if randomseed is true
        WORLDGRID = Biome.StartGeneration(WorldSize, SEED);


        PaintLevel();
    }

    private void PaintLevel() // will move to a seperate script later
    {
        //Tilemap.SetTile(Vector3Int.zero, tile);
        foreach (Vector2Int pos in WorldSize.allPositionsWithin)
        {
            if (WORLDGRID[pos.x, pos.y] == (int)TileType.wall) Tilemap.SetTile((Vector3Int)pos, tile);
            
            switch (WORLDGRID[pos.x, pos.y])
            {
                case (int)TileType.wall:
                    tile.color = Color.white;
                    Tilemap.SetTile((Vector3Int)pos, tile);
                    break;

                case (int)TileType.corridor:
                    tile.color = Color.grey;
                    Tilemap.SetTile((Vector3Int)pos, tile);
                    break;
            }
        }
    } 
}

public enum TileType
{
    air,
    wall,
    corridor
}
