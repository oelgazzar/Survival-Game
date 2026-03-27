using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] GameObject _inventoryScreen;
    [SerializeField] InventoryItem _inventoryItemPrefab;

    public static InventorySystem Instance;

    readonly List<ItemSlot> _slots = new();
    readonly List<InventoryItemData> _items = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in _inventoryScreen.transform.GetChild(1))
        {
            if (child.TryGetComponent<ItemSlot>(out var slot))
            {
                _slots.Add(slot);
            }
        }
    }

    ItemSlot FindNextEmptySlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }

    public bool TryAddItem(InventoryItemData inventoryItemData)
    {
        if (inventoryItemData == null)
        {
            Debug.LogError("Inventory Item Data is null");
            return false;
        }

        var slot = FindNextEmptySlot();
        if (slot == null)
        {
            Debug.Log("inventory is full");
            return false;
        }

        _items.Add(inventoryItemData);

        var item = Instantiate(_inventoryItemPrefab, slot.transform);
        item.SetData(inventoryItemData);

        return true;
    }
}