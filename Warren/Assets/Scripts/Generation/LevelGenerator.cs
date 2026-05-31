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
        //Tilemap.ClearAllTiles();
        CreateWorldGrid();

        SEED = RandomSeed? UnityEngine.Random.Range(1,999) : SEED; //generates a seed if randomseed is true
        WORLDGRID = Biome.StartGeneration(WORLDGRID, WorldSize, SEED);


        PaintLevel();
    }

    private void PaintLevel() // will move to a seperate script later
    {
        foreach (Vector2Int pos in WorldSize.allPositionsWithin)
        {
            if (Tilemap.GetTile((Vector3Int)pos) != null) continue;

            switch (WORLDGRID[pos.x, pos.y])
            {
                case (int)TileType.wall:
                    tile.color = Color.white;
                    Tilemap.SetTile((Vector3Int)pos, tile);
                    break;

                case (int)TileType.corridor:
                    tile.color = Color.darkGray;
                    Tilemap.SetTile((Vector3Int)pos, tile);
                    break;

                case (int)TileType.air:
                    Tilemap.SetTile((Vector3Int)pos, null);
                    break;
            }
        }
    }

    void CreateWorldGrid()
    {
        WORLDGRID = new int[WorldSize.size.x, WorldSize.size.y];

        for (int x= 0;  x < WORLDGRID.GetLength(0); x++)
        {
            for (int y= 0; y < WORLDGRID.GetLength(1); y++)
            {
                if (Tilemap.GetTile(new(x, y, 0)) != null) WORLDGRID[x, y] = (int)TileType.air;
                else WORLDGRID[x, y] = (int)TileType.wall;
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
