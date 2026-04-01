using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class QuickSlotUI : SlotUI
{
    [SerializeField] TMP_Text _numberText;

    private void Awake()
    {
        SlotType = SlotUIType.QuickSlot;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent<InventoryItemUI>(out var itemUI))
        {
            InventorySystem.Instance.AddItemToQuickSlot(itemUI.InventorySlotIndex, Index);
        }
    }

    public override void SetIndex(int index)
    {
        base.SetIndex(index);
        _numberText.text = (index + 1).ToString();
    }

    public override void SetData(InventorySlot inventorySlot)
    {
        base.SetData(inventorySlot);
        if (inventorySlot != null && inventorySlot.IsEquipped)
        {
            _numberText.color = Color.white;
        } else
        {
            _numberText.color = Color.white * .5f;
        }
    }
}
