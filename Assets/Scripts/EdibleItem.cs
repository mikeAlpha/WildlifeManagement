using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItem : Item
{
    public override void PerformAction() { }

    public override void InitialiseItem(Action action)
    {
        base.InitialiseItem(action);
        Debug.Log(ItemName + " is consumed");
    }
}
