using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject _inventoryScreen;
    [SerializeField] GameObject _craftingScreen;
    [SerializeField] GameObject _toolsScreen;

    AudioSource _audioSource;
    readonly Dictionary<KeyControl, int> _keySlotMap = new()
    {
        {Keyboard.current.digit1Key, 0},
        {Keyboard.current.digit2Key, 1},
        {Keyboard.current.digit3Key, 2},
        {Keyboard.current.digit4Key, 3},
        {Keyboard.current.digit5Key, 4},
        {Keyboard.current.digit6Key, 5},
        {Keyboard.current.digit7Key, 6},
    };

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
            ToggleInventory(_craftingScreen.activeSelf || _toolsScreen.activeSelf);
        }

        Pause(_inventoryScreen.activeSelf || _craftingScreen.activeSelf || _toolsScreen.activeSelf);

        CheckQuickSlotInputs();
    }

    private void CheckQuickSlotInputs()
    {
        foreach(var key in _keySlotMap.Keys)
        {
            if (key.wasPressedThisFrame)
            {
                var quickSlotIndex = _keySlotMap[key];
                InventorySystem.Instance.UseItemAtQuickSlot(quickSlotIndex);
            }
        }
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
