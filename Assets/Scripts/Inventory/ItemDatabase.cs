using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public InventoryItemData[] inventoryItemDatas;
    public Dictionary<string, InventoryItemData> InventoryItemMap;

    private void OnEnable()
    {
        InventoryItemMap = new Dictionary<string, InventoryItemData>();

        foreach(var item in inventoryItemDatas)
        {
            InventoryItemMap[item.ItemID] = item;
        }

        Debug.Log("ItemDatabase initialized with " + InventoryItemMap.Count + " items.");
    }

    public InventoryItemData GetItemData(string itemID) => InventoryItemMap[itemID];
}
