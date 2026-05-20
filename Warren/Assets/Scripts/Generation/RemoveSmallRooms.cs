using System.Collections.Generic;
using UnityEngine;

public class RemoveSmallRooms 
{
    List<List<Vector3Int>> rooms = new();
    int[,] map;

    public RemoveSmallRooms(int[,] map)
    {
        this.map = map;
    }

    public void FloodFill(int tileToSearch)
    {
        rooms.Clear();
        bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)];

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == tileToSearch && !visited[x, y]) // if is tile and has not been visited
                {
                    // new room

                    List<Vector3Int> room = new();
                    Queue<Vector3Int> q = new();

                    q.Enqueue(new Vector3Int(x, y, 0));
                    visited[x, y] = true;

                    while (q.Count > 0)
                    {
                        var tile = q.Dequeue();
                        room.Add(tile); // add tile to room

                        foreach (var neighbour in neighbours) // for each neighbour
                        {
                            int newX = tile.x + neighbour.x;
                            int newY = tile.y + neighbour.y;

                            // error catching
                            if (newX < 0 || newY < 0 ||
                                newX >= map.GetLength(0) ||
                                newY >= map.GetLength(1))
                                continue;

                            // if neighbour is floor, add to queue
                            if (!visited[newX, newY] && map[newX, newY] == tileToSearch)
                            {
                                visited[newX, newY] = true;
                                q.Enqueue(new Vector3Int(newX, newY, 0));
                            }
                        }
                    }
                    rooms.Add(room);

                }
            }
        }
    }

    public void RemoveRooms(int minSize, int fill)
    {
        if (rooms.Count == 0) return;

        var roomCache = new List<List<Vector3Int>>(rooms);
       
        foreach(var room in roomCache)
        {
            if (room.Count > minSize) continue;

            foreach (var tile in room)
            {
                map[tile.x, tile.y] = fill;
            }

            rooms.Remove(room);
        }
    }

    public List<List<Vector3Int>> GetRooms()
    {
        return rooms;
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
