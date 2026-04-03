using UnityEngine;
using UnityEngine.InputSystem;

public class AxeTool : Tool
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public override void Use()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Swing");
        }
    }

    // Animation Event
    public void OnImpact()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.SphereCast(ray, .2f, out var hit, 3, LayerMask.GetMask("Tree")))
        {
            var tree = hit.collider.GetComponentInParent<ChoppableTree>();
            if (tree != null)
            {
                tree.DealDamage();
            }
        }
    }
}
