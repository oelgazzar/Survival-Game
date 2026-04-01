using UnityEngine.EventSystems;

public class InventorySlotUI : SlotUI
{

    private void Awake()
    {
        SlotType = SlotUIType.InventorySlot;
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent<InventoryItemUI>(out var itemUI))
        {
            if (itemUI.SlotType == SlotUIType.QuickSlot)
            {
                InventorySystem.Instance.ClearQuickSlot(itemUI.InventorySlotIndex);
            } else
            {
                InventorySystem.Instance.MoveItem(itemUI.InventorySlotIndex, Index);
            }
        }
    }
}

