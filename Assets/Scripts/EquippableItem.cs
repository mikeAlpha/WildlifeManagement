using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItem : Item
{
    public override void PerformAction() 
    {
        Debug.Log(ItemName + " is on action");
    }

    public override void InitialiseItem(Action action)
    {
        base.InitialiseItem(action);
        Debug.Log(ItemName + " is equipped");
        EventHandler.ExecuteEvent<EquippableItem>("SetEquippableItem", this);
    }

}
