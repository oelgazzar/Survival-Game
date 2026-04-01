using System;

[Serializable]
public class InventorySlot {
    public int SlotID;
    public InventoryItemData Item;
    public int Amount;

    public InventorySlot(int id) { 
        SlotID = id;
    }
}
