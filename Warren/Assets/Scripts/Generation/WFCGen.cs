using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFCGen : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile tile;

    [SerializeField] List<Module> Modules;
    [SerializeField] Vector2Int GridSize;

    Dictionary<Vector2Int, Cell> Grid;

    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GenerateMap();
        }
    } // press x to regenerate map

    private void GenerateMap()
    {
        CreateGrid();
        StartGeneration();
        PaintCells();
    }

    private void CreateGrid()
    {
        Grid = new Dictionary<Vector2Int, Cell>();

        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Grid.Add(new(x, y), new Cell(Modules, Grid, new(x, y)));
            }
        }

        tilemap.ClearAllTiles();
    }

    private void StartGeneration()
    {
        Cell startCell = Grid[new(
            UnityEngine.Random.Range(0, GridSize.x),
            UnityEngine.Random.Range(0, GridSize.y)
        )];

        startCell.CollapseCell();
    }

    private void PaintCells()
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Module selectedModule = Grid[new(x, y)].SelectedModule;
                tile.color = selectedModule.Colour;
                tilemap.SetTile(new(x, y), tile);
            }
        }
    }
}

class Cell
{
    List<Module> Domain;
    Dictionary<Vector2Int, Cell> Grid;
    Vector2Int Position;

    public Module SelectedModule;
    bool Collapsed;

    Vector2Int[] neighbours = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    };

    public Cell(List<Module> domain, Dictionary<Vector2Int, Cell> grid, Vector2Int pos)
    {
        Domain = new(domain);
        Grid = grid;
        Position = pos;
    }

    public void CollapseCell()
    {
        int random = UnityEngine.Random.Range(0, Domain.Count - 1);
        Debug.Log(Domain.Count);

        SelectedModule = Domain[random];
        Collapsed = true;

        CollapseNeighbours();

        Cell smallestDomain = new(new(50), Grid, new(999,999));
        foreach (Vector2Int n in neighbours)
        {   
            Vector2Int pos = n + Position;

            if (!Grid.ContainsKey(pos)) continue;
            if (Grid[pos].Collapsed) continue;

            if (Grid[pos].Domain.Count < smallestDomain.Domain.Count)
            {
                smallestDomain = Grid[pos];
            } else if (Grid[pos].Domain.Count == smallestDomain.Domain.Count)
            {
                if (UnityEngine.Random.value > 0.5)
                {
                    smallestDomain = Grid[pos];
                } // randomly selects the smallest domain
            }
        }
        smallestDomain.CollapseCell();
    }

    private void CollapseNeighbours()
    {
        foreach (Vector2Int n in neighbours)
        {
            Vector2Int p = n + Position;

            if (!Grid.ContainsKey(p)) continue;
            
            Grid[p].ReduceDomain(SelectedModule);
        }
    }

    public void ReduceDomain(Module module)
    {
        List<Module> domain = new(Domain);

        foreach (Module d in domain)
        {
            if (!module.AdjacentModules.Contains(d.ModuleID))
            {
                Domain.Remove(d);
            }
        }
    }
}

[Serializable]
public class Module
{
    public int ModuleID;
    public int[] AdjacentModules;

    public Color Colour;
}
