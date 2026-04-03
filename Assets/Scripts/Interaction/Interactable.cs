using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] InventoryItemData _inventoryItemData;

    public string Name => _inventoryItemData?.Name;
    public Sprite HoverIcon => _inventoryItemData?.HoverCursorIcon;

    public void Interact()
    {
        if (InventorySystem.Instance.TryAddItem(_inventoryItemData))
        {
            Destroy(gameObject);
        }
    }
}
