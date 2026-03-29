using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] GameObject _slotsPanel;
    [SerializeField] GameObject _itemRemovalConfirmationBox;
    [SerializeField] Button _itemRemovalConfirmationButton;
    [SerializeField] Button _itemRemovalCancelButton;
    
    readonly List<InventorySlotUI> _slotsUI = new();

    InventoryItemUI _itemToBeRemoved;

    private void Start()
    {
        PopulateSlotUIList();

        _itemRemovalConfirmationButton.onClick.AddListener(RemoveItem);
        _itemRemovalCancelButton.onClick.AddListener(
            () => ToggleItemRemovalBox(false));
    }

    private void RemoveItem()
    {
        if (_itemToBeRemoved != null)
        {
            InventorySystem.Instance.RemoveItemAt(_itemToBeRemoved.SlotIndex);
            _itemToBeRemoved = null;
            ToggleItemRemovalBox(false);
        }
    }

    private void PopulateSlotUIList()
    {
        var index = 0;

        foreach (Transform child in _slotsPanel.transform)
        {
            if (child.TryGetComponent<InventorySlotUI>(out var slotUI))
            {
                slotUI.Index = index;
                _slotsUI.Add(slotUI);
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

    // Called by Drop Event Trigger On Canvas
    public void ConfirmItemRemoval(BaseEventData eventData)
    {
        var e = eventData as PointerEventData;
        var draggedItem = e.pointerDrag;
        if ( draggedItem.TryGetComponent<InventoryItemUI>(out var itemUI)) {
            _itemToBeRemoved = itemUI;
            ToggleItemRemovalBox(true);
        }
    }

    private void ToggleItemRemovalBox(bool value)
    {
        _itemRemovalConfirmationBox.SetActive(value);
    }

    private void OnEnable()
    {
        InventorySystem.InventoryChanged += UpdateInventorySlotsUI;
    }

    private void OnDisable()
    {
        InventorySystem.InventoryChanged -= UpdateInventorySlotsUI;
        _itemRemovalConfirmationButton.onClick.RemoveAllListeners();
        _itemRemovalCancelButton.onClick.RemoveAllListeners();
    }
}
