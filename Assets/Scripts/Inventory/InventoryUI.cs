using UnityEngine;
using System.Collections.Generic;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] GameObject _slotsPanel;
    
    readonly List<InventorySlotUI> _slotsUI = new();

    private void Start()
    {
        PopulateSlotUIList();
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

    private void OnEnable()
    {
        InventorySystem.InventoryChanged += UpdateInventorySlotsUI;
    }

    private void OnDisable()
    {
        InventorySystem.InventoryChanged -= UpdateInventorySlotsUI;
    }
}
