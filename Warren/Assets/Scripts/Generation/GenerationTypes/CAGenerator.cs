using System.ComponentModel;
using UnityEngine;

public class CAGenerator : GenAlgorithm
{ 
    [Header("Cellular Automata")]
    [SerializeField, Range(0, 1)] float RandomTileChance;
    [SerializeField] float Scale = 20f;
    [SerializeField] int NeighbourRequirement;
    [SerializeField] int Passes;

    [Header("Small Rooms")]
    [SerializeField] int minRoomSize;

    [Header("Corrdiors")]
    [SerializeField] int corridorThreshold;

    int[,] gridCache;
    int[,] newCache;

    int seed;

    public override int[,] StartGeneration(int[,] WORLDGRID, BoundsInt bounds, int seed)
    {
        this.bounds = bounds;
        this.seed = seed;

        gridCache = WORLDGRID.Clone() as int[,];
        newCache = WORLDGRID.Clone() as int[,]; ;

        GenerateRandomTiles();
        SmoothGeneration();

        RemoveSmallRooms RSR = new(gridCache); // removes small pockets of air
        RSR.FloodFill((int)TileType.air);
        RSR.RemoveRooms(minRoomSize, (int)TileType.wall);

        ConnectRooms CR = new ConnectRooms(RSR.GetRooms(), gridCache, corridorThreshold);

        RSR.FloodFill((int)TileType.wall);
        RSR.RemoveRooms(2, (int)TileType.air);

        return gridCache;
    }   

    public void GenerateRandomTiles()
    {
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (gridCache[pos.x, pos.y] == (int)TileType.air) { continue; } 

            Vector3Int localPos = pos - bounds.min;

            if (localPos.x <= bounds.min.x || localPos.x >= bounds.max.x - 1 || 
                localPos.y <= bounds.min.y || localPos.y >= bounds.max.y - 1) 
            { 
                gridCache[localPos.x, localPos.y] = (int)TileType.wall; 
                continue; 
            }   
            float xCoord = (float)localPos.x / bounds.size.x * Scale + seed;
            float yCoord = (float)localPos.y / bounds.size.y * Scale + seed;

            gridCache[localPos.x, localPos.y] = Mathf.PerlinNoise(xCoord, yCoord) 
                < RandomTileChance ? (int)TileType.wall : (int)TileType.air; // uses perlin noise to detirmin tile type
        }
    }

    private void SmoothGeneration()
    {
        for (int i = 0; i < Passes; i++)
        {
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                AdjustCell(pos);
            }

            gridCache = newCache;
            newCache = new int[bounds.size.x, bounds.size.y];
        }
    }

    private void AdjustCell(Vector3Int tile)
    {
        int wallCount = 0;
        foreach (Vector3Int neighbour in neighbours)
        {
            Vector3Int t = tile + neighbour;

            if (t.x <= bounds.min.x || t.x >= bounds.max.x) { continue; }
            if (t.y <= bounds.min.y || t.y >= bounds.max.y) { continue; }

            Vector3Int newTile = t - bounds.min;

            int getTile = gridCache[newTile.x, newTile.y];
            if (getTile == (int)TileType.air) // how many neighbours
            {
                wallCount++;
            }
        }

        tile -= bounds.min;

        if (wallCount > NeighbourRequirement) { newCache[tile.x, tile.y] = (int)TileType.air; }
        else if (wallCount < NeighbourRequirement) { newCache[tile.x, tile.y] = (int)TileType.wall; }
        else { newCache[tile.x, tile.y] = gridCache[tile.x, tile.y]; }
    }
}
