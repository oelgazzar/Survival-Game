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
            InventorySystem.Instance.MoveItem(itemUI.InventorySlotIndex, Index);
        }
    }
}

