using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen, _survivalScreen, _refineAndProcessScreen;
    [SerializeField] GameObject _toolsPanel, _survivalPanel, _refineAndProcessPanel;
    [SerializeField] Button _toolsButton, _survivalButton, _refineAndProcessButton;
    [SerializeField] RecipeUI _recipeUIPrefab;

    readonly List<RecipeUI> _toolsRecipeUIs = new();
    readonly List<RecipeUI> _survivalRecipeUIs = new();
    readonly List<RecipeUI> _refineAndProcessRecipeUIs = new();

    CraftingCategory _currentCategory;

    private void Start()
    {
        _toolsButton.onClick.AddListener(OpenToolsScreen);
        _survivalButton.onClick.AddListener(OpenSurvivalScreen);
        _refineAndProcessButton.onClick.AddListener(OpenRefineAndProcessScreen);
        
        InitRecipeUIs();
    }

    private void InitRecipeUIs()
    {
        for (var i = 0; i < CraftingSystem.Instance.Recipes.Length; i++)
        {
            var recipe = CraftingSystem.Instance.Recipes[i];
            var category = recipe.Category;

            switch(category)
            {
                case CraftingCategory.Tools:
                    var recipeUI = Instantiate(_recipeUIPrefab, _toolsPanel.transform);
                    recipeUI.SetRecipe(recipe);
                    _toolsRecipeUIs.Add(recipeUI);
                    break;
                case CraftingCategory.Survival:
                    var survivalRecipeUI = Instantiate(_recipeUIPrefab, _survivalPanel.transform);
                    survivalRecipeUI.SetRecipe(recipe);
                    _survivalRecipeUIs.Add(survivalRecipeUI);
                    break;
                case CraftingCategory.RefineAndProcess:
                    var refineAndProcessRecipeUI = Instantiate(_recipeUIPrefab, _refineAndProcessPanel.transform);
                    refineAndProcessRecipeUI.SetRecipe(recipe);
                    _refineAndProcessRecipeUIs.Add(refineAndProcessRecipeUI);
                    break;
            }
        }
    }

    void OpenToolsScreen()
    {
        _craftingScreen.SetActive(false);
        _toolsScreen.SetActive(true);
        _survivalScreen.SetActive(false);
        _refineAndProcessScreen.SetActive(false);
        
        _currentCategory = CraftingCategory.Tools;
        UpdateRecipesData();
    }
    private void OpenSurvivalScreen()
    {
        _craftingScreen.SetActive(false);
        _toolsScreen.SetActive(false);
        _survivalScreen.SetActive(true);
        _refineAndProcessScreen.SetActive(false);
        
        _currentCategory = CraftingCategory.Survival;
        UpdateRecipesData();
    }
    private void OpenRefineAndProcessScreen()
    {
        _craftingScreen.SetActive(false);
        _toolsScreen.SetActive(false);
        _survivalScreen.SetActive(false);
        _refineAndProcessScreen.SetActive(true);
        
        _currentCategory = CraftingCategory.RefineAndProcess;
        UpdateRecipesData();
    }


    void UpdateRecipesData(List<InventorySlot> _ = null)
    {
        switch(_currentCategory)
        {
            case CraftingCategory.Tools:
                foreach (var recipeUI in _toolsRecipeUIs)
                {
                    recipeUI.UpdateRecipeData();
                }
                break;
            case CraftingCategory.Survival:
                foreach (var recipeUI in _survivalRecipeUIs)
                {
                    recipeUI.UpdateRecipeData();
                }
                break;
            case CraftingCategory.RefineAndProcess:
                foreach (var recipeUI in _refineAndProcessRecipeUIs)
                {
                    recipeUI.UpdateRecipeData();
                }
                break;
        }
    }
    private void OnEnable()
    {
        InventorySystem.InventoryChanged += UpdateRecipesData;
    }
    private void OnDisable()
    {
        InventorySystem.InventoryChanged -= UpdateRecipesData;
    }

}
