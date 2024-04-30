using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Spade", menuName = "Assets/InventoryItem/Spade")]
public class Spade : EquippableItem
{
    public TileBase soilTile;
    public override void PerformAction()
    {
        base.PerformAction();
        Vector3 pos = InputManager.instance.getMousePositon;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        Vector3 worldClickPosition = ray.origin + ray.direction;
        var gridManager = GridManager.instance;
        List<Vector3Int> getAllAdacentTiles = gridManager.GetAdjacentTiles();
        Vector3Int gridPosition = gridManager.getTilemap.WorldToCell(worldClickPosition);
        gridPosition.z = 0;

        Debug.Log("gridPosition=====" + gridPosition);

        if(getAllAdacentTiles.Contains(gridPosition))
            gridManager.ChangeTileTextureAtPosition(gridPosition, soilTile);
    }
}
