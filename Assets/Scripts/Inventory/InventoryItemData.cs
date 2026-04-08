using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItems/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public string ItemID;
    public string Name;
    public ItemInfo Info;
    public InventoryItemType ItemType;
    public Sprite Sprite;
    public Sprite HoverCursorIcon;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ItemID))
        {
            ItemID = Guid.NewGuid().ToString();
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
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
