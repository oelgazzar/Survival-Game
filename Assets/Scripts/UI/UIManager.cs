using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _interactionInfoText;
    [SerializeField] Image _screenCenterIcon;
    [SerializeField] Vector2 _defaulIconSize = new(5, 5);
    [SerializeField] Vector2 _altIconSize = new(30, 30);
    [SerializeField] GameObject _pauseMenu;

    public Sprite DefaultScreenCenterIcon;


    public static UIManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateInteractionInfoText(string text)
    {
        _interactionInfoText.gameObject.SetActive(text != null);
        if (_interactionInfoText.gameObject.activeSelf)
            _interactionInfoText.text = text;
    }

    public void UpdateScreenCenterIcon(Sprite hoverCursorIcon)
    {
        _screenCenterIcon.enabled = hoverCursorIcon != null;
        if (_screenCenterIcon.enabled)
            _screenCenterIcon.sprite = hoverCursorIcon;

        _screenCenterIcon.rectTransform.sizeDelta = hoverCursorIcon == DefaultScreenCenterIcon ? _defaulIconSize : _altIconSize;
    }

    public void TogglePauseMenu(bool value)
    {
        _pauseMenu.SetActive(value);
    }
}
