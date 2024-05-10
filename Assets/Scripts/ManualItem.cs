using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualItem : ScriptableObject
{
    public string ItemName;
    public GameObject Render;
    public string ItemDescription;
    public BoundsInt area;

    public void InitialiseItem(Action action)
    {
        var obj = Instantiate(Render);
        obj.SetActive(false);
        EventHandler.ExecuteEvent<GameObject, BoundsInt>("SetCurrentObject",  obj, area);
        action();
    }
}
