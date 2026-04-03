using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] TMP_Text _interactionInfoText;
    [SerializeField] float _raycastDistance = 10f;
    [SerializeField] float _raycastRadius = .1f;
    [SerializeField] Image _screenCenter;
    [SerializeField] Sprite _defaultIcon;
    [SerializeField] Vector2 _defaulIconSize = new (5, 5);
    [SerializeField] Vector2 _altIconSize = new(30, 30);

    public static InteractionManager Instance;

    public Interactable Target;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused)
        {
            SetScreenCenterImage(null);
            return;
        }

        var ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.SphereCast(ray, _raycastRadius, out var hit, _raycastDistance))
        {
            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                _interactionInfoText.text = interactable.Name;
                _interactionInfoText.gameObject.SetActive(true);
                Target = interactable;
                SetScreenCenterImage(interactable.HoverIcon);
                return;
            }
        }
        _interactionInfoText.gameObject.SetActive(false);
        Target = null;
        SetScreenCenterImage(_defaultIcon);
    }

    void SetScreenCenterImage(Sprite sprite)
    {
        _screenCenter.enabled = sprite != null;
        _screenCenter.sprite = sprite;
        _screenCenter.rectTransform.sizeDelta = sprite == _defaultIcon? _defaulIconSize : _altIconSize;
    }
}