using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryItemUI))]
public class InventoryItemDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Canvas _canvas;

    Transform _startParent;

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
    }

    private void OnEnable()
    {
        _canvasGroup.blocksRaycasts = true;
    }
}
