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
            Hover(false);
            Destroy(gameObject);
        }

        return true;
    }
}
