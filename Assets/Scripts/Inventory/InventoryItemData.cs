using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
}
