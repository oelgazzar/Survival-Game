using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _amountText;
    [SerializeField] Image _equippedIndicator;

    public int InventorySlotIndex;
    public SlotUIType SlotType;
    public InventoryItemData Data;
    public void SetData(InventoryItemData inventoryItemData, int Amount, bool isEquipped)
    {
        Data = inventoryItemData;
        _icon.sprite = Data.Sprite;
        _icon.preserveAspect = true;
        if (SlotType == SlotUIType.InventorySlot)
        {
            _amountText.text = Amount.ToString();
        } else
        {
            _amountText.text = "";
        }

        _equippedIndicator.enabled = isEquipped && SlotType == SlotUIType.InventorySlot;

        GetComponent<InventoryItemDragHandler>().enabled =
            !(isEquipped && SlotType == SlotUIType.QuickSlot);
    }
}

public enum SlotUIType { 
    InventorySlot, QuickSlot
}
