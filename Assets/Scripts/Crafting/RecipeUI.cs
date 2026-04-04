using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] TMP_Text _NameText;
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text[] _ingredientTexts;
    [SerializeField] Button _craftButton;

    CraftingRecipe _recipe;

    private void Start()
    {
        _craftButton.onClick.AddListener(Craft);
    }

    private void Craft()
    {
        CraftingSystem.Instance.Craft(_recipe);
    }

    public void SetRecipe(CraftingRecipe recipe)
    {
        _recipe = recipe;
        _NameText.text = _recipe.Result.Name;
        _icon.sprite = _recipe.Result.Sprite;

    }

    public void UpdateRecipeData()
    {
        var fulfilledIngredients = 0;

        for (var i = 0; i < _recipe.Ingredients.Count; i++)
        {
            var ingredient = _recipe.Ingredients[i];
            var itemCount = InventorySystem.Instance.GetItemCount(ingredient.Item);
            _ingredientTexts[i].text = $"- {ingredient.Amount} {ingredient.Item.Name} ({itemCount})";
            if (itemCount >= ingredient.Amount)
            {
                fulfilledIngredients++;
            }
        }

        _craftButton.interactable = (fulfilledIngredients == _recipe.Ingredients.Count);
    }
}
