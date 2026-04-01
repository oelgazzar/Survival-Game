using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] int _capacity = 21;
    [SerializeField] int _numQuickSlots = 7;

    public static InventorySystem Instance;

    public static event Action<List<InventorySlot>> InventoryChanged;
    public static event Action<InventorySlot[]> QuickSlotsUpdated;
    public static event Action<InventoryItemData> ItemAddedToInventory;

    readonly List<InventorySlot> _inventorySlots = new();
    InventorySlot[] _quickSlots;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitSlots();
    }

    void InitSlots()
    {
        for (var i = 0; i < _capacity; i++)
        {
            _inventorySlots.Add(new InventorySlot(i));
        }
        _quickSlots = new InventorySlot[_numQuickSlots];
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

        InventoryChanged?.Invoke(_inventorySlots);
        ItemAddedToInventory?.Invoke(inventoryItemData);

        return true;
    }

    InventorySlot FindNextEmptySlot()
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.Item == null)
                return slot;
        }
        return null;
    }

    public bool HasItem(InventoryItemData item, int amount)
    {
        var available = 0;
        foreach (var slot in _inventorySlots)
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

    public int GetItemCount(InventoryItemData item)
    {
        var count = 0;

        foreach (var slot in _inventorySlots)
        {
            if (slot.Item == item)
            {
                count += slot.Amount;
            }
        }
        return count;
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

        foreach (var slot in _inventorySlots)
        {
            if (slot.Item == item)
            {
                var removed = Mathf.Min(remaining, slot.Amount);
                slot.Amount -= removed;
                remaining -= removed;

                if (slot.Amount == 0)
                {
                    slot.Item = null;
                    ClearQuickSlot(slot.SlotID);
                }

                if (remaining == 0)
                    break;
            }
        }

        InventoryChanged?.Invoke(_inventorySlots);
    }

    public void RemoveItemAt(int index)
    {
        var slot = _inventorySlots[index];
        if (slot.Item != null)
        {
            slot.Amount--;
            if (slot.Amount <= 0)
            {
                slot.Item = null;
                slot.Amount = 0;
            }
        }

        ClearQuickSlot(index);

        InventoryChanged?.Invoke(_inventorySlots);
    }

    // Drag and drop functionality
    public void MoveItem(int fromIndex, int toIndex)
    {
        (_inventorySlots[fromIndex], _inventorySlots[toIndex]) = (_inventorySlots[toIndex], _inventorySlots[fromIndex]);

        UpdateSlotsIDs();

        InventoryChanged?.Invoke(_inventorySlots);
    }

    private void UpdateSlotsIDs()
    {
        for (var i = 0; i < _inventorySlots.Count; i++)
        {
            var slot = _inventorySlots[i];
            slot.SlotID = i;
        }
    }

    public void UseItem(int slotIndex)
    {
        var item = _inventorySlots[slotIndex].Item;
        if (item != null)
        {
            switch(item.ItemType)
            {
                case InventoryItemType.Consumable:
                    var consumable = item as ConsumableInventoryItemData;
                    if ( consumable != null &&
                        PlayerStatusManager.Instance.TryApplyConsumableToStatus(consumable))
                    {
                        RemoveItemAt(slotIndex);
                    }
                    break;

            }
        }
    }

    public void UseItemAtQuickSlot(int quickSlotIndex)
    {
        if (_quickSlots[quickSlotIndex] != null)
        {
            var inventorySlot = _quickSlots[quickSlotIndex];
            for (var i = 0; i < _inventorySlots.Count; i++)
            {
                if (_inventorySlots[i] == inventorySlot)
                {
                    UseItem(i);
                    return;
                }
            }
        }
    }

    public void AddItemToQuickSlot(int inventorySlotIndex, int quickSlotIndex)
    {
        // Prevent duplicates
        ClearQuickSlot(inventorySlotIndex);

        var inventorySlot = _inventorySlots[inventorySlotIndex];

        _quickSlots[quickSlotIndex] = inventorySlot;

        QuickSlotsUpdated?.Invoke(_quickSlots);
    }

    public void ClearQuickSlot(int inventorySlotIndex)
    {
        var inventorySlot = _inventorySlots[inventorySlotIndex];
        
        for (var i = 0; i < _quickSlots.Length; i++)
        {
            var quickSlot = _quickSlots[i];
            if (inventorySlot == quickSlot)
            {
                _quickSlots[i] = null;
                break;
            }
        }
        QuickSlotsUpdated?.Invoke(_quickSlots);
    }

    private void SyncQuickSlot(List<InventorySlot> _)
    {
        QuickSlotsUpdated?.Invoke(_quickSlots);
    }

    private void OnEnable()
    {
        InventoryChanged += SyncQuickSlot;
    }

    private void OnDisable()
    {
        InventoryChanged += SyncQuickSlot;        
    }
}