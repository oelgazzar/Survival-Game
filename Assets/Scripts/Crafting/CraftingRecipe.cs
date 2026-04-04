using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Scriptable Objects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public CraftingCategory Category;
    public InventoryItemData Result;
    public List<CraftingIngredient> Ingredients;
}

public enum CraftingCategory
{
    Tools,
    Survival,
    RefineAndProcess
}
