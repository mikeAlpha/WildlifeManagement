using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _manualBookUiContainer;

    [SerializeField]
    private Transform _inventoryUiContainer;

    [SerializeField]
    private ManualList _manualList;

    [SerializeField]
    private InventoryList _inventoryList;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _manualList.SetupItems(_manualBookUiContainer);
        _inventoryList.SetupItems(_inventoryUiContainer);
    }

    public void ToggleManualBookUI(bool val)
    {
        _manualBookUiContainer.parent.gameObject.SetActive(val);
    }

    public void ToggleInventoryUI(bool val)
    {
        _inventoryUiContainer.parent.gameObject.SetActive(val);
    }
}
