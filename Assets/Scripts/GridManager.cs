using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    private Tilemap tilemap;
    public Tilemap getTilemap { get { return tilemap; } }

    private Vector3Int playerPosition;
    private List<Vector3Int> adjacentTiles = new List<Vector3Int>();

    List<Node> nodes;

    [SerializeField] private int NodeSizeX, NodeSizeY;

    [HideInInspector]
    public Grid grid;

    public static GridManager instance;
    public TileBase tile, pathTile;

    AStarPathfinding pathFinding;
    Vector3Int gridWorldCellPosition;
    int maxX, maxY, minX, minY;

    [SerializeField]
    private float tilesPerFrame;

    private void OnEnable()
    {
        EventHandler.RegisterEvent<Vector2Int, Vector2Int>("OnFindingPath", InitPathFinding);
    }

    private void OnDisable()
    {
        EventHandler.UnregisterEvent<Vector2Int, Vector2Int>("OnFindingPath", InitPathFinding);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        tilemap = GetComponentInChildren<Tilemap>();
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        StartCoroutine(GenerateNodes());
    }

    public void InitPathFinding(Vector2Int startPos, Vector2Int endPos)
    {
        pathFinding = new AStarPathfinding(this);
        var path = pathFinding.FindPath(startPos, endPos);

        //for (int i = 0; i < path.Count; i++)
        //{
        //    tilemap.SetTile(new Vector3Int(path[i].position.x, path[i].position.y), pathTile);
        //}
    }

    IEnumerator GenerateNodes()
    {
        nodes = new List<Node>();

        maxX = gridWorldCellPosition.x + NodeSizeX - 1;
        maxY = gridWorldCellPosition.y + NodeSizeY - 1;
        minX = gridWorldCellPosition.x;
        minY = gridWorldCellPosition.y;


        for (int x = 0; x < NodeSizeX; x++)
        {
            for(int y = 0; y<NodeSizeY; y++)
            {
                gridWorldCellPosition = grid.WorldToCell(new Vector3(344, 184));
                var pos = new Vector2Int(x, y) + new Vector2Int(gridWorldCellPosition.x, gridWorldCellPosition.y);
                tilemap.SetTile(new Vector3Int(pos.x,pos.y), tile);
                nodes.Add(new Node(pos, true));

                float progress = ((float)(x * NodeSizeY + y) / (NodeSizeX * NodeSizeY)) * 100f;

                Debug.Log("Progress====" + progress);

                if ((x * NodeSizeY + y) % tilesPerFrame == 0)
                    yield return null;
            }
        }
    }

    void SetTileWalkableOrNot(Vector2Int pos, bool val)
    {
        if(nodes != null)
        {
            nodes.FirstOrDefault(x => x.position == pos).IsWalkable = val;
        }
    }

    private void CalculateAdjacentTilesFromPlayerPosition()
    {
        adjacentTiles.Clear();

        Vector3Int[] directions = {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (Vector3Int direction in directions)
        {
            Vector3Int adjacentTilePosition = this.playerPosition + direction;
            TileBase tile = tilemap.GetTile(adjacentTilePosition);
            if (tile != null)
            {
                //Vector3Int tilepos = tilemap.WorldToCell(adjacentTilePosition);
                adjacentTiles.Add(adjacentTilePosition);
            }
        }
    }

    public void SetPlayerPosition(Vector3 playerPosition)
    {
        this.playerPosition = grid.WorldToCell(playerPosition);
        CalculateAdjacentTilesFromPlayerPosition();
    }

    public List<Vector3Int> GetAdjacentTiles()
    {
        return adjacentTiles;
    }

    public void ChangeTileTextureAtPosition(Vector3Int position, TileBase newTile)
    {
        tilemap.SetTile(position, newTile);
    }

    public Node ConvertToNodeFromWorldPos(Vector2Int pos)
    {
        return nodes.FirstOrDefault(x => x.position == pos);
    }

    public List<Node> GenerateNeighbours(Node n)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int cGridX = n.position.x + x;
                int cGridY = n.position.y + y;

                var pos = new Vector2Int(cGridX, cGridY);

                if (cGridX >= minX && cGridX < maxX && cGridY >= minY && cGridY < maxY)
                {
                    Debug.Log("Neighbours====" + pos);
                    neighbors.Add(nodes.FirstOrDefault(x => x.position == pos));
                }
            }
        }

        return neighbors;
    }
}
