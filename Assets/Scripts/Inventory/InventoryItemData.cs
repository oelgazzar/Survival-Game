using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItems/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public string Name;
    public InventoryItemType ItemType;
    public Sprite Sprite;
}

public enum InventoryItemType
{
    Material, Consumable, Equippable, QuestItem
}
