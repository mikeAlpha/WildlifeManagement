using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class AssetFinder
{
    public static List<T> FindAssetsByType<T>() where T : Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        List<T> assets = new List<T>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }
}
