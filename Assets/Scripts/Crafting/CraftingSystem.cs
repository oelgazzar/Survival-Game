using System;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    public CraftingRecipe[] Recipes;

    private void Awake()
    {
        Instance = this;
    }

    public void Craft(CraftingRecipe recipe)
    {
        var inventory = InventorySystem.Instance;
        if (CanCraft(recipe))
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                inventory.RemoveItem(ingredient.Item, ingredient.Amount);
            }
            inventory.TryAddItem(recipe.Result);
        }
    }

    bool CanCraft(CraftingRecipe recipe)
    {
        var inventory = InventorySystem.Instance;
        foreach (var ingredient in recipe.Ingredients)
        {
            if (inventory.HasItem(ingredient.Item, ingredient.Amount) == false) {
                return false;
            }
        }
        return true;
    }
}
