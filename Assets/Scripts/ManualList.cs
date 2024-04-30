using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ManualList",menuName = "Assets/CreateManualList")]
public class ManualList : ScriptableObject
{
    public List<ManualItem> items = new List<ManualItem>();

    public GameObject item_prefab;

    public void SetupItems(Transform container)
    {
        if(items != null && items.Count > 0)
        {
            foreach(ManualItem i in items)
            {
                var g = Instantiate(item_prefab, container);
                g.GetComponentInChildren<UnityEngine.UI.Text>().text = i.ItemName;
                Action a = () => UIManager.instance.ToggleManualBookUI(false);
                g.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>i.InitialiseItem(a));
            }
        }
    }
}
