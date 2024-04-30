using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectPlacementManager : MonoBehaviour
{
    private GameObject currentObject;
    private BoundsInt area;

    private void OnEnable()
    {
        EventHandler.RegisterEvent<GameObject, BoundsInt>("SetCurrentObject", SetCurrentObjectToPlace);
        EventHandler.RegisterEvent<Vector3>("SetCurrentPositionofObj", SetCurrentPositionofObj);
        EventHandler.RegisterEvent("DestroyCurrentObject", DestroyCurrentObject);
    }

    private void OnDisable()
    {
        EventHandler.UnregisterEvent<GameObject, BoundsInt>("SetCurrentObject", SetCurrentObjectToPlace);
        EventHandler.UnregisterEvent<Vector3>("SetCurrentPositionofObj", SetCurrentPositionofObj);
        EventHandler.UnregisterEvent("DestroyCurrentObject", DestroyCurrentObject);
    }

    void SetCurrentObjectToPlace(GameObject obj, BoundsInt area)
    {
        this.currentObject = obj;
        this.area = area;
        this.currentObject.SetActive(true);
        InputManager.instance.SetobjectPlacing();
    }

    void SetCurrentPositionofObj(Vector3 pos)
    {
        Vector3Int cellPos = GridManager.instance.grid.WorldToCell(pos);
        Vector3 tilepos = GridManager.instance.getTilemap.GetCellCenterWorld(cellPos);
        area.position = cellPos;

        foreach(var posi in area.allPositionsWithin)
        {
            Debug.Log("Main position + "+ area.position +"position ==" + posi.x + " " + posi.y);
        }
        
      

        currentObject.transform.position = tilepos;
    }

    void DestroyCurrentObject()
    {
        if (currentObject != null)
        {
            Destroy(currentObject);
            currentObject = null;
        }
    }
}
