using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Canvas _canvas;
    Image _image;
    
    Vector2 _startPos;
    Transform _startParent;
    InventoryItemData _inventoryItemData;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _image = GetComponent<Image>();
        _image.sprite = _inventoryItemData.Sprite;
    }

    public void SetData(InventoryItemData inventoryItemData)
    {
        _inventoryItemData = inventoryItemData;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = _rectTransform.anchoredPosition;
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
            _rectTransform.anchoredPosition = _startPos;
        }
        _canvasGroup.blocksRaycasts = true;
    }
}
