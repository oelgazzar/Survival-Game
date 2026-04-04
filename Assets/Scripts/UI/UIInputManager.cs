using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] GameObject _inventoryScreen;
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen, _survivalScreen, _refineAndProcessScreen;

    AudioSource _audioSource;
    Dictionary<KeyControl, int> _keySlotMap;

    bool _isInventoryOpen => _inventoryScreen.activeSelf;
    bool _isCraftingScreensOpen => _craftingScreen.activeSelf || _toolsScreen.activeSelf || 
        _survivalScreen.activeSelf || _refineAndProcessScreen.activeSelf;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;

        _keySlotMap= new()
        {
            {Keyboard.current.digit1Key, 0},
            {Keyboard.current.digit2Key, 1},
            {Keyboard.current.digit3Key, 2},
            {Keyboard.current.digit4Key, 3},
            {Keyboard.current.digit5Key, 4},
            {Keyboard.current.digit6Key, 5},
            {Keyboard.current.digit7Key, 6},
        };
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory(!_isInventoryOpen);

        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCraftingScreen(!_isCraftingScreensOpen);
            ToggleInventory(_isCraftingScreensOpen);
        }

        Pause(_isInventoryOpen || _isCraftingScreensOpen);

        CheckQuickSlotInputs();
    }

    private void CheckQuickSlotInputs()
    {
        foreach(var key in _keySlotMap.Keys)
        {
            if (key.wasPressedThisFrame)
            {
                var quickSlotIndex = _keySlotMap[key];
                InventorySystem.Instance.EquipItem(quickSlotIndex);
            }
        }
    }

    public void ToggleInventory(bool value)
    {
        _inventoryScreen.SetActive(value);
        _audioSource.Play();
    }

    public void ToggleCraftingScreen(bool value)
    {
        _craftingScreen.SetActive(value);
        _toolsScreen.SetActive(false);
        _survivalScreen.SetActive(false);
        _refineAndProcessScreen.SetActive(false);
        _audioSource.Play();
    }

    private void Pause(bool value)
    {
        Cursor.lockState = value? CursorLockMode.None : CursorLockMode.Locked;
        GameManager.Instance.Pause(value);
    }
}
