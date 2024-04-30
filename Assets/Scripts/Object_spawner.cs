using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Object_spawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] objectsToPlace;
    public int numberOfObjects;
    public Vector2Int minBounds;
    public Vector2Int maxBounds;


    public List<GameObject> objs_on_map = new List<GameObject>();

    public void PlaceObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3Int randomPosition = new Vector3Int(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
             0
            );
            Vector3 tileCenter = tilemap.GetCellCenterWorld(randomPosition);
            int randomIndex = Random.Range(0, objectsToPlace.Length);
            var name = objectsToPlace[randomIndex] + "_" + i;
            var objs = objs_on_map.Where(p => p.name == name).ToList();
            Debug.Log(objs.Count + "====" + name);
            if (objs.Count == 1)
            {
                objs_on_map[i].transform.position = tileCenter;
            }
            else
            {
                GameObject obj = Instantiate(objectsToPlace[randomIndex], tileCenter, Quaternion.identity);
                obj.name = name;
                obj.transform.parent = this.transform;
                objs_on_map.Add(obj);
            }
        }
    }

    public void ClearDestroy()
    {
        for(int i = 0; i<objs_on_map.Count; i++)
        {
            DestroyImmediate(objs_on_map[i]);
        }
        objs_on_map.Clear();
    }
}
