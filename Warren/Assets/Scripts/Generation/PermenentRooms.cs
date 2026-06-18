using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using JetBrains.Annotations;

public class PermenentRooms : MonoBehaviour
{
    [SerializeField] Room[] PrefabRooms;
    [SerializeField] string EntranceRoom;
    [SerializeField] string ArtefactRoom;

    Vector3Int artefactSpawnPoint;
    [SerializeField] Tilemap Floormap;
    [SerializeField] Tilemap Wallmap;
    int[,] WorldGrid;

    public int[,] PlacePrefabRooms(BoundsInt worldSize, int offset, int[,] worldGrid)
    {
        // foreach room in prefabsrooms
        // Get room bounds from floor map
        // decide spawn position
        // if room name is entrance room, set artifact room's position
        // if room is artefact, spawn at set position
        // use bounds to loop through RoomToSpawn's children tilemap
        // if floor map, covert tile to int[,]
        // for all maps, copy over to tilemap

        WorldGrid = worldGrid; 

        foreach (Room room in PrefabRooms)
        {
            GameObject roomToSpawn = room.RoomToSpawn;
            Vector3Int spawn = room.SpawnAreas[UnityEngine.Random.Range(0, room.SpawnAreas.Count)] + (Vector3Int.one * offset);

            BoundsInt bounds = new BoundsInt(Vector3Int.zero, Vector3Int.one);
            
            foreach (Transform t in roomToSpawn.transform) // foreach tilemap layer
            {
                Tilemap layer = t.GetComponent<Tilemap>();

                if (layer.name == Floormap.name)
                {
                    bounds = new (Vector3Int.zero,layer.cellBounds.size);

                    PasteLayer(layer.GetComponent<Tilemap>(), Floormap.GetComponent<Tilemap>(), spawn, bounds);
                }
                else if (layer.name == Wallmap.name)
                {
                    // wall map

                    PasteLayer(layer.GetComponent<Tilemap>(), Wallmap.GetComponent<Tilemap>(), spawn, bounds);
                }


            }
        }

        return worldGrid;
    }

    /*BoundsInt GetBounds(GameObject room)
    {

    }*/

    void PasteLayer(Tilemap source, Tilemap target, Vector3Int spawnpoint, BoundsInt bounds)
    {
        foreach (var pos in bounds.allPositionsWithin)
        {
            Vector3Int localPos = pos + spawnpoint;
            target.SetTile(localPos, source.GetTile(pos));

            if (target.name == Floormap.name) // if tilemap is floor
            {
                print($"{spawnpoint}, {localPos}");
                WorldGrid[localPos.x, localPos.y] = (int)TileType.air;
            }

        }
    }

    Vector3Int GetOppositeFromPoint(Vector3Int point)
    {
        return Vector3Int.zero;
    }
}

[CreateAssetMenu(fileName = "new room", menuName = "Scriptable Objects/ Premade Room")]
public class Room : ScriptableObject
{
    public string RoomName;
    public GameObject RoomToSpawn;
    //public BoundsInt Size = new BoundsInt(Vector3Int.zero, Vector3Int.one);

    public List<Vector3Int> SpawnAreas = new List<Vector3Int>();
}
