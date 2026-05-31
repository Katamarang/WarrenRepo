using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectRooms 
{
    int[,] Map;

    Queue<(Vector3Int, int)> unconnected = new Queue<(Vector3Int, int)>(); // unconnected room centers and size
    Dictionary<Vector3Int, List<Vector3Int>> connections = new Dictionary<Vector3Int, List<Vector3Int>>();

    public ConnectRooms(List<List<Vector3Int>> rooms, int[,] map, int threshold)
    {
        Map = map;

        foreach (var room in rooms)
        {
            Vector3Int center = GetRoomCenter(room);
            unconnected.Enqueue((center, room.Count));

            connections.Add(center, new List<Vector3Int>());
            //Map[center.x, center.y] = (int)TileType.corridor;
        }    

        while (unconnected.Count > 0)
        {
            var r = unconnected.Dequeue();

            int connectionAmount = ConnectionAmount(r.Item2, threshold);

            for (int i = 0; i < connectionAmount; i++)
            {
                Vector3Int closestRoom = r.Item1;
                float closestDistance = float.MaxValue;

                foreach (var room in unconnected)
                {                    
                    if (connections[r.Item1].Contains(room.Item1)) { continue; }

                    Vector3Int center = room.Item1;

                    float distance = (center - r.Item1).magnitude;

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestRoom = center;
                    }
                }

                connections[r.Item1].Add(closestRoom);
                CarveCorridor(r.Item1, closestRoom, connectionAmount);
            }
        }
    }

    void CarveCorridor(Vector3Int a, Vector3Int b, int CorridorSize)
    {
        Vector3Int current = a;
        int i = 0;

        while (current != b && i != 999)
        {
            i++;

            CarveCross(current, CorridorSize);

            int dx = b.x - current.x;
            int dy = b.y - current.y;

            if (UnityEngine.Random.value < 0.3f)
            {
                if (UnityEngine. Random.value < 0.5f) dx = 0;
                else dy = 0;
            }

            if (dx != 0) current.x += Mathf.Clamp(dx, -1, 1);
            else if (dy != 0) current.y += Mathf.Clamp(dy, -1, 1);
        }
    }

    void CarveCross(Vector3Int pos, int size)
    {
        Map[pos.x, pos.y] = (int)TileType.air;

        if (size == 1) return;

        foreach (Vector3Int n in neighbours)
        {
            Vector3Int npos = n + pos;
            Map[npos.x, npos.y] = (int)TileType.air;
        }
    }

    Vector3Int GetRoomCenter(List<Vector3Int> room)
    {
        int x = 0, y = 0;
        foreach (var p in room)
        {
            x += p.x;
            y += p.y;
        }
        return new Vector3Int(x / room.Count, y / room.Count, 0);
    }

    int ConnectionAmount(int roomSize, int threshold)
    {
        if (roomSize < threshold) { return 1; }
        else if (roomSize >= threshold) { return 2; }
        else if (roomSize >= threshold * 2) { return 3; }
        else { return 4; }
    }

    Vector2Int[] neighbours = new Vector2Int[]
    {
        new(1,0),
        new(0,1)
        //new(-1,0),
        //new(0,-1)
    };
}
