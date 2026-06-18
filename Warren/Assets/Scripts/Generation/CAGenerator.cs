using System;
using System.ComponentModel;
using UnityEngine;

public class CAGenerator : MonoBehaviour
{
    // Algorithm that uses cellular automata to generate cave like structures.
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
    BoundsInt bounds;
    int[,] WorldGrid;

    public int[,] StartGeneration(int[,] worldGrid, BoundsInt bounds, int seed, out Vector3Int start)
    {
        this.bounds = bounds;
        this.seed = seed;
        WorldGrid = worldGrid;

        gridCache = new int[bounds.size.x,bounds.size.y];
        newCache = new int[bounds.size.x, bounds.size.y];

        GenerateRandomTiles(); // uses perlin noise to generate random tiles
        SmoothGeneration(); // uses cellular automata to smooth the generated tiles

        RemoveSmallRooms RSR = new(gridCache); // removes small pockets of air
        RSR.FloodFill((int)TileType.air);
        RSR.RemoveRooms(minRoomSize, (int)TileType.wall);

        ConnectRooms CR = new ConnectRooms(RSR.GetRooms(), gridCache, corridorThreshold); // connects the remaining rooms with corridors

        RSR.FloodFill((int)TileType.wall); // removes small pockets of wall
        RSR.RemoveRooms(2, (int)TileType.air);

        start = bounds.min;
        return gridCache;
    }   

    public void GenerateRandomTiles()
    {
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (WorldGrid[pos.x, pos.y] == (int)TileType.air) continue;

            Vector3Int localPos = pos - bounds.min;
            
            float xCoord = (float)pos.x / bounds.size.x * Scale + seed;
            float yCoord = (float)pos.y / bounds.size.y * Scale + seed;

            gridCache[localPos.x, localPos.y] = Mathf.PerlinNoise(xCoord, yCoord) 
                < RandomTileChance ? (int)TileType.wall : (int)TileType.air; // uses perlin noise to determine the tile type.
        }
    }

    private void SmoothGeneration()
    {
        for (int i = 0; i < Passes; i++)
        {
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                if (WorldGrid[pos.x, pos.y] == (int)TileType.air) continue;

                AdjustCell(pos);
            }

            gridCache = newCache;
            newCache = new int[bounds.size.x, bounds.size.y];
        }
    }

    private void AdjustCell(Vector3Int tile) // adjusts cell based on its neighbours.
    {
        int wallCount = 0;
        foreach (Vector3Int neighbour in neighbours)
        {
            Vector3Int t = tile + neighbour;

            if (t.x < bounds.min.x || t.x >= bounds.max.x) { continue; }
            if (t.y < bounds.min.y || t.y >= bounds.max.y) { continue; }

            Vector3Int newTile = t - bounds.min;

            int getTile = gridCache[newTile.x, newTile.y];
            if (getTile == (int)TileType.air) // how many neighbours
            {
                wallCount++;
            }
        }

        tile -= bounds.min;

        // if wallCount is greater than the requirement, make it air, if its less, make it wall, otherwise keep it the same.
        if (wallCount > NeighbourRequirement) { newCache[tile.x, tile.y] = (int)TileType.air; }
        else if (wallCount < NeighbourRequirement) { newCache[tile.x, tile.y] = (int)TileType.wall; }
        else { newCache[tile.x, tile.y] = gridCache[tile.x, tile.y]; }
    }

    Vector2Int[] neighbours = new Vector2Int[]
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
}
