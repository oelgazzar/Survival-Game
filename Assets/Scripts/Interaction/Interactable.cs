using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] InventoryItemData _inventoryItemData;

    Outline _outline;
    public bool PlayerInRange;
    public string Name => _inventoryItemData.Name;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        if ( _outline != null )
            _outline.enabled = false;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && PlayerInRange && InteractionManager.Instance.Target == this)
        {
            if (InventorySystem.Instance.TryAddItem(_inventoryItemData))
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

}
