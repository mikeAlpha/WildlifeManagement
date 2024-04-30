using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryList))]
public class InventoryListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var obj = target as InventoryList;


        if (GUILayout.Button("Get All Inventory Items"))
        {
            obj.items = AssetFinder.FindAssetsByType<Item>();

            Debug.Log(obj.items.Count);

            foreach(var item in obj.items)
            {
                Debug.Log(item.name + " added");
            }
        }

    }
}
