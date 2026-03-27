using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] InventoryItemUI _inventoryItemUIPrefab;

    public int Index;

    InventoryItemUI _itemUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent<InventoryItemUI>(out var itemUI))
        {
            InventorySystem.Instance.MoveItem(itemUI.SlotIndex, this.Index);
        }
    }

    public void SetData(InventorySlot inventorySlot)
    {
        if (inventorySlot.Item == null)
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
            _itemUI.SlotIndex = Index;
        }

        _itemUI.gameObject.SetActive(true);
        // In case dragged away
        _itemUI.transform.SetParent(transform);
        _itemUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        _itemUI.SetData(inventorySlot.Item, inventorySlot.Amount);
    }
}
