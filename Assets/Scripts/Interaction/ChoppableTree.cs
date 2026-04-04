using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ChoppableTree : Interactable
{
    [SerializeField] GameObject _logsPrefab;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _damage = 50;
    [SerializeField] AudioClip[] _chopSfx;
    [SerializeField] AudioClip _choppedSfx;

    ResourceBar _resourceBar;
    AudioSource _audioSource;
    Camera _cam;

    float _health;
    bool _isChopped;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _health = _maxHealth;
        _resourceBar = SceneContext.Instance.ResourceBar;
        _cam = Camera.main;
    }
    public override void Hover(bool value)
    {
        if (value == true)
        {
            _resourceBar.UpdateName(_name);
            _resourceBar.UpdateValue(_health / _maxHealth);
        }

        _resourceBar.gameObject.SetActive(value && !_isChopped);
    }

    public void Hit()
    {
        if (_isChopped) return;

        _health -= _damage;
        
        StopAllCoroutines();
        StartCoroutine(Shake());

        var randomIndex = Random.Range(0, _chopSfx.Length);
        _audioSource.PlayOneShot(_chopSfx[randomIndex]);

        if (_health <= 0)
        {
            _isChopped = true;
            _audioSource.PlayOneShot(_choppedSfx);
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Shake()
    {
        var startPos = transform.position;

        var magnitude = .003f;

        var timer = .2f;

        while (timer > 0)
        {
            var direction = Random.insideUnitCircle;
            transform.Translate(new Vector3(direction.x, 0, direction.y) * magnitude);
            timer -= Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }

    IEnumerator Fall()
    {
        var timer = .5f;

        transform.LookAt(_cam.transform.position + _cam.transform.forward*5f);

        while (timer > 0)
        {
            var rotationX = Mathf.Lerp(0, 90, 1- (timer / .5f));
            transform.rotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, transform.eulerAngles.z);
            timer -= Time.deltaTime;
            yield return null;
        }

        GetComponentInChildren<Collider>().enabled = false;
        Instantiate(_logsPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, .1f);
    }
}
