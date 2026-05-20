using System.Collections.Generic;
using UnityEngine;

public class ConnectRooms 
{
    List<List<Vector3Int>> Rooms;
    int[,] Map;

    Queue<Vector3Int> unconnected = new Queue<Vector3Int>(); // unconnected room centers
    List<Vector3Int> connected = new List<Vector3Int>(); // connected room centers

    public ConnectRooms(List<List<Vector3Int>> rooms, int[,] map)
    {
        Rooms = rooms;
        Map = map;

        foreach (var room in rooms)
        {
            unconnected.Enqueue(GetRoomCenter(room));
        }    

        while (unconnected.Count > 0)
        {
            var room = unconnected.Dequeue();
            connected.Add(room);

            Vector3Int closestRoom = room;
            float closestDistance = float.MaxValue;

            foreach (var center in unconnected)
            {
                float distance = Vector3.Distance(room, center);
                if (closestDistance > distance) { closestRoom = center; closestDistance = distance; }
            }

            CarveCorridor(room, closestRoom);
        }
    }

    void CarveCorridor(Vector3Int a, Vector3Int b)
    {
        Vector3Int current = a;
        int i = 0;


        while (current != b && i != 999)
        {
            i++;

            Map[current.x, current.y] = (int)TileType.corridor;

            int dx = Mathf.Clamp(b.x - current.x, -1, 1);
            int dy = Mathf.Clamp(b.y - current.y, -1, 1);

            current = new Vector3Int(current.x + dx, current.y + dy, 0);
        }

        Map[current.x, current.y] = (int)TileType.corridor;
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
}
