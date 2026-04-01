using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] GameObject _slotsPanel;
    [SerializeField] GameObject _quickSlotsPanel;
    [SerializeField] GameObject _itemRemovalConfirmationBox;
    [SerializeField] Button _itemRemovalConfirmationButton;
    [SerializeField] Button _itemRemovalCancelButton;

    readonly List<InventorySlotUI> _slotsUI = new();
    readonly List<QuickSlotUI> _quickSlotsUI = new();

    InventoryItemUI _itemToBeRemoved;

    private void Start()
    {
        PopulateInventorySlotUIList();
        PopulateQuickSlotUIList();

        _itemRemovalConfirmationButton.onClick.AddListener(RemoveItem);
        _itemRemovalCancelButton.onClick.AddListener(
            () => ToggleItemRemovalBox(false));
    }


    private void PopulateInventorySlotUIList()
    {
        var index = 0;

        foreach (Transform child in _slotsPanel.transform)
        {
            if (child.TryGetComponent<InventorySlotUI>(out var slotUI))
            {
                slotUI.SetIndex(index);
                _slotsUI.Add(slotUI);
                index++;
            }
        }
    }

    private void PopulateQuickSlotUIList()
    {
        var index = 0;

        foreach (Transform child in _quickSlotsPanel.transform)
        {
            if (child.TryGetComponent<QuickSlotUI>(out var quickSlotUI))
            {
                quickSlotUI.SetIndex(index);
                _quickSlotsUI.Add(quickSlotUI);
                index++;
            }
        }
    }

    void UpdateInventorySlotsUI(List<InventorySlot> slots)
    {
        for (var i = 0; i < _slotsUI.Count; i++)
        {
            _slotsUI[i].SetData(slots[i]);
        }
    }

    private void UpdateQuickSlotsUI(InventorySlot[] quickSlots)
    {
        for (var i = 0; i < _quickSlotsUI.Count; i++)
        {
            _quickSlotsUI[i].SetData(quickSlots[i]);
        }
    }

    // Called by Drop Event Trigger On Canvas
    public void ConfirmItemRemoval(BaseEventData eventData)
    {
        var e = eventData as PointerEventData;
        var draggedItem = e.pointerDrag;
        if ( draggedItem.TryGetComponent<InventoryItemUI>(out var itemUI)) {
            if (itemUI.SlotType == SlotUIType.QuickSlot)
            {
                InventorySystem.Instance.ClearQuickSlot(itemUI.InventorySlotIndex);
            }
            else if (itemUI.SlotType == SlotUIType.InventorySlot)
            {
                _itemToBeRemoved = itemUI;
                ToggleItemRemovalBox(true);
            }
        }
    }

    private void ToggleItemRemovalBox(bool value)
    {
        _itemRemovalConfirmationBox.SetActive(value);
    }

    private void RemoveItem()
    {
        if (_itemToBeRemoved != null)
        {
            InventorySystem.Instance.RemoveItemAt(_itemToBeRemoved.InventorySlotIndex);
            _itemToBeRemoved = null;
            ToggleItemRemovalBox(false);
        }
    }

    private void OnEnable()
    {
        InventorySystem.InventoryChanged += UpdateInventorySlotsUI;
        InventorySystem.QuickSlotsUpdated += UpdateQuickSlotsUI;
    }

    private void OnDisable()
    {
        InventorySystem.InventoryChanged -= UpdateInventorySlotsUI;
        InventorySystem.QuickSlotsUpdated -= UpdateQuickSlotsUI;
        _itemRemovalConfirmationButton.onClick.RemoveAllListeners();
        _itemRemovalCancelButton.onClick.RemoveAllListeners();
    }
}
