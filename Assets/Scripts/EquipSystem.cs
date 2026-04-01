using System;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance;
    public event Action<EquippableInventoryItemData, bool> ItemEquipped;

    public EquippableInventoryItemData _equippedItem;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleEquipItem(EquippableInventoryItemData equppiable, bool isEquipped)
    {
        _equippedItem = isEquipped ? equppiable : null;

        ItemEquipped?.Invoke(equppiable, isEquipped);
    }
}
