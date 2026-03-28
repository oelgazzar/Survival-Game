using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySystem : MonoBehaviour
{
    /// <summary>
    /// for testing purposes
    [SerializeField] InventoryItemData _debugStoneItemData;
    [SerializeField] InventoryItemData _debugStringItemData;
    /// </summary>
    [SerializeField] int _capacity = 21;

    public static InventorySystem Instance;
    public static event Action<List<InventorySlot>> InventoryChanged;
    public static event Action<InventoryItemData> ItemAddedToInventory;

    readonly List<InventorySlot> _slots = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitSlotList();
    }

    void InitSlotList()
    {
        for (var i = 0; i < _capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    InventorySlot FindNextEmptySlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.Item == null)
                return slot;
        }
        return null;
    }

    public int GetItemCount(InventoryItemData item)
    {
        var count = 0;

        foreach (var slot in _slots)
        {
            if (slot.Item == item)
            {
                count += slot.Amount;
            }
        }
        return count;
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

        slot.Item = inventoryItemData;
        slot.Amount = 1;

        InventoryChanged?.Invoke(_slots);
        ItemAddedToInventory?.Invoke(inventoryItemData);

        return true;
    }

    public bool HasItem(InventoryItemData item, int amount)
    {
        var available = 0;
        foreach (var slot in _slots)
        {
            if (slot.Item == item)
            {
                available += slot.Amount;

                if (available >= amount)
                    break;
            }
        }

        return available >= amount;
    }

    public void RemoveItem(InventoryItemData item, int amount)
    {
        if (amount < 0)
            return;

        if (HasItem(item, amount) == false)
        {
            Debug.Log("This item not available in this amount");
            return;
        }

        var remaining = amount;

        foreach (var slot in _slots)
        {
            if (slot.Item == item)
            {
                var removed = Mathf.Min(remaining, slot.Amount);
                slot.Amount -= removed;
                remaining -= removed;

                if (slot.Amount == 0)
                {
                    slot.Item = null;
                }

                if (remaining == 0)
                    break;
            }
        }

        InventoryChanged?.Invoke(_slots);
    }

    // Debug methods
    [ContextMenu("Test Remove 0 stones")]
    void RemoveZeroStones()
    {
        RemoveItem(_debugStoneItemData, 0);
    }

    [ContextMenu("Test Remove 5 stones")]
    void RemoveFiveStones()
    {
        RemoveItem(_debugStoneItemData, 5);
    }

    // Drag and drop functionality
    public void MoveItem(int fromIndex, int toIndex)
    {
        (_slots[fromIndex], _slots[toIndex]) = (_slots[toIndex], _slots[fromIndex]);

        InventoryChanged?.Invoke(_slots);
    }
}