using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryItemUI))]
public class InventoryItemDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] TMP_Text _amountText;
    [SerializeField] Image _equippedIndicator;

    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Canvas _canvas;

    Transform _startParent;

    bool _startEquippedIndicatorState;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startParent = transform.parent;
        transform.SetParent(_canvas.transform, true);
        _canvasGroup.blocksRaycasts = false;

        _startEquippedIndicatorState = _equippedIndicator.enabled;
        _amountText.enabled = false;
        _equippedIndicator.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == _canvas.transform)
        {
            transform.SetParent(_startParent);
            _rectTransform.anchoredPosition = Vector2.zero;
        }
        _canvasGroup.blocksRaycasts = true;

        _amountText.enabled = true;
        _equippedIndicator.enabled = _startEquippedIndicatorState;
    }

    private void OnEnable()
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
