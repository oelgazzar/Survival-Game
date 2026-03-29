using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _amountText;

    public int SlotIndex;

    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Canvas _canvas;
    
    Transform _startParent;
    InventoryItemData _inventoryItemData;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(InventoryItemData inventoryItemData, int Amount)
    {
        _inventoryItemData = inventoryItemData;
        _icon.sprite = _inventoryItemData.Sprite;
        _amountText.text = Amount.ToString();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventorySystem.Instance.Consume(SlotIndex);
        }
    }
}
