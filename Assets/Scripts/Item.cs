using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;

    public string ItemDescription;

    public Sprite ItemImage;

    public bool IsStackable = false;

    public int MaxSize;

    public abstract void PerformAction();

    public virtual void InitialiseItem(Action action)
    {
        action();
    }
}
