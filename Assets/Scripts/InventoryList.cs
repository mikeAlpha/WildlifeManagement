using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryList", menuName = "Assets/CreateInventoryList")]
public class InventoryList : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public GameObject item_prefab;

    public void SetupItems(Transform container)
    {
        if (items != null && items.Count > 0)
        {
            foreach (Item i in items)
            {
                var g = Instantiate(item_prefab, container);
                g.GetComponentInChildren<UnityEngine.UI.Text>().text = i.ItemName;
                Action a = () => UIManager.instance.ToggleInventoryUI(false);
                g.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => i.InitialiseItem(a));
            }
        }
    }
}
