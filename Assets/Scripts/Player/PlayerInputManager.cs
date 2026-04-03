using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public InputAction MouseMoveAction;
    public InputAction MoveAction;
    public InputAction JumpAction;

    InteractionManager _interactionManager;
    EquipmentManager _equipmentManager;

    private void Awake()
    {
        _interactionManager = GetComponent<InteractionManager>();
        _equipmentManager = GetComponent<EquipmentManager>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_interactionManager.Target != null)
            {
                _interactionManager.Target.Interact();
            } else
            {
                _equipmentManager.UseEquippedTool();
            }
        }
    }

    private void OnEnable()
    {
        MouseMoveAction.Enable();
        MoveAction.Enable();
        JumpAction.Enable();
    }

    private void OnDisable()
    {
        MouseMoveAction.Disable();
        MoveAction.Disable();
        JumpAction.Disable();
    }
}
