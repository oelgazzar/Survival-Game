using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject _inventoryScreen;
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen;

    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory(!_inventoryScreen.activeSelf);

        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCraftingScreen(!_craftingScreen.activeSelf && !_toolsScreen.activeSelf);
        }

        Pause(_inventoryScreen.activeSelf || _craftingScreen.activeSelf || _toolsScreen.activeSelf);
    }

    public void ToggleInventory(bool open)
    {
        _inventoryScreen.SetActive(open);
        _audioSource.Play();
    }

    public void ToggleCraftingScreen(bool open)
    {
        _craftingScreen.SetActive(open);
        _toolsScreen.SetActive(false);
        _audioSource.Play();
    }

    private void Pause(bool value)
    {
        Cursor.lockState = value? CursorLockMode.None : CursorLockMode.Locked;
        GameManager.Instance.Pause(value);
    }
}
