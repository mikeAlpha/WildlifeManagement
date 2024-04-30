using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private EquippableItem current_item;

    private void SetEquippableItem(EquippableItem item)
    {
        this.current_item = item;
    }

    private void ExecuteItemAction()
    {
        if (current_item != null)
            current_item.PerformAction();
    }
}
