using UnityEngine;

public class Pickable : Interactable
{
    public InventoryItemData ItemData;

    private void Start()
    {
        _name = ItemData.Name;
        _hoverCursorIcon = ItemData.HoverCursorIcon;
    }

    public override bool Interact()
    {
        if (InventorySystem.Instance.TryAddItem(ItemData))
        {
            // Disable collider to prevent multiple interactions while the item is being picked up
            GetComponent<Collider>().enabled = false;
            gameObject.SetActive(false); // Hide the object immediately for better feedback
            Destroy(gameObject);
        }
        return true;
    }
}
