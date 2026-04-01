using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _amountText;

    public int InventorySlotIndex;
    public SlotUIType SlotType;
    public InventoryItemData Data;

    public void SetData(InventoryItemData inventoryItemData, int Amount)
    {
        Data = inventoryItemData;
        _icon.sprite = Data.Sprite;
        _icon.preserveAspect = true;
        _amountText.text = Amount.ToString();
    }
}

public enum SlotUIType { 
    InventorySlot, QuickSlot
}
