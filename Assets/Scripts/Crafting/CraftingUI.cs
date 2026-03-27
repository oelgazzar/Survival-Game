using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen;
    [SerializeField] GameObject _toolsPanel;
    [SerializeField] Button _toolsButton;
    [SerializeField] RecipeUI _recipeUIPrefab;

    readonly List<RecipeUI> _recipeUIs = new();

    private void Start()
    {
        _toolsButton.onClick.AddListener(OpenToolsScreen);

        for (var i = 0; i < CraftingSystem.Instance.Recipes.Length; i++)
        {
            var recipeUI = Instantiate(_recipeUIPrefab, _toolsPanel.transform);
            recipeUI.SetRecipe(CraftingSystem.Instance.Recipes[i]);
            _recipeUIs.Add(recipeUI);
        }
    }

    void OpenToolsScreen()
    {
        _craftingScreen.SetActive(false);
        _toolsScreen.SetActive(true);
        UpdateRecipesData();
    }

    void UpdateRecipesData(List<InventorySlot> _ = null)
    {
        foreach (var recipeUI in _recipeUIs)
        {
            recipeUI.UpdateRecipeData();
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
