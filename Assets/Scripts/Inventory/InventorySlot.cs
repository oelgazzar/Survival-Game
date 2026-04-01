using System;

[Serializable]
public class InventorySlot {
    public int SlotID;
    public InventoryItemData Item;
    public int Amount;
    public bool IsEquipped;

    public InventorySlot(int id) { 
        SlotID = id;
    }
}
