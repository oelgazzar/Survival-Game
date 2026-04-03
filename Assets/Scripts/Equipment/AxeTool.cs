using UnityEngine;

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
}
