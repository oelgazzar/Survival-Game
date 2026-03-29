using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryItemUI))]
public class InventoryItemRightClickHandler : MonoBehaviour, IPointerClickHandler
{
    InventoryItemUI _inventoryItemUI;

    private void Awake()
    {
        _inventoryItemUI = GetComponent<InventoryItemUI>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventorySystem.Instance.UseItem(_inventoryItemUI.SlotIndex);
        }
    }
}
