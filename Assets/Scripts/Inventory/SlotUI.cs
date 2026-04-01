using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SlotUI : MonoBehaviour, IDropHandler {
    [SerializeField] protected InventoryItemUI _inventoryItemUIPrefab;

    public int Index { get; private set; }
    protected SlotUIType SlotType;

    protected InventoryItemUI _itemUI;

    public abstract void OnDrop(PointerEventData eventData);

    public virtual void SetIndex(int index)
    {
        Index = index;
    }

    public virtual void SetData(InventorySlot inventorySlot)
    {
        if (inventorySlot == null || inventorySlot.Item == null)
        {
            if (_itemUI != null)
            {
                _itemUI.gameObject.SetActive(false);
            }
            return;
        }

        if (_itemUI == null)
        {
            _itemUI = Instantiate(_inventoryItemUIPrefab, transform);
            _itemUI.InventorySlotIndex = Index;
        }

        _itemUI.gameObject.SetActive(true);
        // In case dragged away
        _itemUI.transform.SetParent(transform);
        _itemUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        _itemUI.InventorySlotIndex = inventorySlot.SlotID;
        _itemUI.SlotType = SlotType;
        _itemUI.SetData(inventorySlot.Item, inventorySlot.Amount);
    }
}

