using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] Transform _toolHolder;

    public static EquipmentManager Instance;
    public event Action<EquippableInventoryItemData, bool> ItemEquipped;

    public Tool _equippedTool;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleEquipItem(EquippableInventoryItemData equppiable, bool isEquipped)
    {
        if (_equippedTool != null)
        {
            Destroy(_equippedTool.gameObject);
        }

        _equippedTool = isEquipped? Instantiate(equppiable.ToolPrefab, _toolHolder) : null;

        ItemEquipped?.Invoke(equppiable, isEquipped);
    }

    public void UseEquippedTool()
    {
        if (_equippedTool == null) return;

        _equippedTool.Use();
    }
}
