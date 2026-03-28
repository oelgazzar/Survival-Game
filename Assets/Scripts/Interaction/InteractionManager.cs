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
    [SerializeField] Sprite _DotImage;
    [SerializeField] Sprite _HandImage;

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
        var ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.SphereCast(ray, _raycastRadius, out var hit, _raycastDistance))
        {
            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null && interactable.PlayerInRange)
            {
                _interactionInfoText.text = interactable.Name;
                _interactionInfoText.gameObject.SetActive(true);
                Target = interactable;
                if (interactable.TryGetComponent<Pickable>(out var _))
                {
                    SetScreenCenterImage(_HandImage);
                }
                return;
            }
        }
        _interactionInfoText.gameObject.SetActive(false);
        Target = null;
        SetScreenCenterImage(_DotImage);
    }

    void SetScreenCenterImage(Sprite sprite)
    {
        _screenCenter.sprite = sprite;

        if (sprite == _DotImage)
        {
            _screenCenter.rectTransform.sizeDelta = Vector2.one * 5;
        } else
        {
            _screenCenter.rectTransform.sizeDelta = Vector2.one * 30;
        }
    }
}