using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen;
    [SerializeField] Button _toolsButton;
    [SerializeField] Button _craftAxeButton;
    [SerializeField] TMP_Text _craftAxeReq1;
    [SerializeField] TMP_Text _craftAxeReq2;
    public static CraftingSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _toolsButton.onClick.AddListener(OpenToolsScreen);
    }

    void OpenToolsScreen()
    {
        _craftingScreen.SetActive(false);
        _toolsScreen.SetActive(true);
    }

}
