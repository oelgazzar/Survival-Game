using UnityEngine;

public class ChoppableTree : Interactable
{
    [SerializeField] ResourceBar _resourceBar;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _damage = 10;

    float _health;

    private void Start()
    {
        _health = _maxHealth;
    }
    public override void Hover(bool value)
    {
        if (value == true)
        {
            _resourceBar.UpdateName(_name);
            _resourceBar.UpdateValue(_health / _maxHealth);
        }

        _resourceBar.gameObject.SetActive(value);
    }

    public void DealDamage()
    {
        _health -= _damage;
        if (_health <= 0)
        {
            Debug.Log("tree chopped");
        }
    }
}
