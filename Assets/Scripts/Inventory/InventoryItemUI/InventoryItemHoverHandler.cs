using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryItemUI))]
public class InventoryItemHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryItemUI _inventoryItemUI;
    ItemInfoUI _itemInfoUI;

    private void Awake()
    {
        _inventoryItemUI = GetComponent<InventoryItemUI>();
        _itemInfoUI = SceneContext.Instance.ItemInfoPanel.GetComponent<ItemInfoUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemInfoUI.gameObject.SetActive(true);
        _itemInfoUI.SetItemInfo(_inventoryItemUI.Data.Info);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemInfoUI.gameObject.SetActive(false);
    }
}
