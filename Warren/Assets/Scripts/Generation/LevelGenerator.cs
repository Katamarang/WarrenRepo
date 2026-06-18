using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{
    // per level script. Handles level generation. Painting the level will be moved to a seperate script later on.

    public int[,] WORLDGRID { get; private set; }

    public int SEED;
    [SerializeField] bool RandomSeed;
    [SerializeField] bool OverwriteCurrent = true;

    [Header("Tilemap")]

    [SerializeField] Tilemap Tilemap;
    [SerializeField] Vector3Int LevelSize; // the size of the level
    [SerializeField] int TileBufferAmount; // the amount of tiles that spawn around the level
    BoundsInt WorldSize;

    CAGenerator Biome;
    PermenentRooms PrefabRooms;

    [SerializeField] Tile tile;

    public static Action<int[,]> GenerateMap;
    public static Action<int[,]> PaintMap;

    private void Start()
    {
        Biome = GetComponent<CAGenerator>();
        PrefabRooms = GetComponent<PermenentRooms>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // debug key to generate level
        {
            GenerateLevel();
        }
    }

    [ContextMenu("Generate Level")]
    public void GenerateLevel()
    {
        WorldSize = new BoundsInt // creates a boundsint of the total worldsize, buffer included
            (
                new(0, 0, 0),
                new(LevelSize.x + (TileBufferAmount * 2), LevelSize.y + (TileBufferAmount * 2), 1)
            );

        if (OverwriteCurrent) ResetLevel();
        CreateWorldGrid();

        MergeGridToWorldGrid(PrefabRooms.PlacePrefabRooms(WorldSize, TileBufferAmount, WORLDGRID), Vector3Int.zero);

        SEED = RandomSeed? UnityEngine.Random.Range(1,999) : SEED; //generates a seed if randomseed is true
        MergeGridToWorldGrid(
            Biome.StartGeneration(WORLDGRID, new(new(TileBufferAmount, TileBufferAmount), LevelSize), SEED, out Vector3Int start),
            start);

        PaintLevel();
    }

    void MergeGridToWorldGrid(int[,] grid, Vector3Int start)
    {
        for (int x = 0;  x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                if (WORLDGRID[x + start.x, y + start.y] == (int)TileType.air) continue;
                WORLDGRID[x + start.x, y + start.y] = grid[x, y];
            }
        }
    }

    private void PaintLevel() // will move to a seperate script later
    {     
        foreach (var pos in WorldSize.allPositionsWithin)
        {
            if (Tilemap.GetTile(pos) != null) continue;
            
            switch (WORLDGRID[pos.x, pos.y])
            {
                case (int)TileType.wall:
                    tile.color = Color.white;
                    Tilemap.SetTile(pos, tile);
                    break;

                case (int)TileType.corridor:
                    tile.color = Color.darkGray;
                    Tilemap.SetTile(pos, tile);
                    break;

                case (int)TileType.air:
                    Tilemap.SetTile(pos, null);
                    break;
            }
        }

        tile.color = Color.red;
        Tilemap.SetTile(WorldSize.max, tile);
    }
    void ResetLevel()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Tilemap>().ClearAllTiles();
        }
    }

    void CreateWorldGrid() // creates the world grid based on the current tilemap. Allows for tiles to be placed in editor.
    {
        WORLDGRID = new int[WorldSize.size.x, WorldSize.size.y];

        foreach (var pos in WorldSize.allPositionsWithin)
        {
            if (Tilemap.GetTile(new(pos.x, pos.y, 0)) != null) WORLDGRID[pos.x, pos.y] = (int)TileType.air;
            else WORLDGRID[pos.x, pos.y] = (int)TileType.wall;
        }
    }

}

public enum TileType
{
    air,
    wall,
    corridor
}
