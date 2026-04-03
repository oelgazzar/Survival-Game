using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItems/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public string Name;
    public ItemInfo Info;
    public InventoryItemType ItemType;
    public Sprite Sprite;
    public Sprite HoverCursorIcon;
}

public enum InventoryItemType
{
    Material, Consumable, Equippable, QuestItem
}

[Serializable]
public class ItemInfo
{
    public string Name;
    public string Description;
    public string Functionality;
}
