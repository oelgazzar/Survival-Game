using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] TMP_Text _messageText;
    [SerializeField] Image _icon;
    [SerializeField] float _fadeDuration = 2;

    public event Action<NotificationUI> NotificationTimedOut;
    string _message;
    float _duration;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Create(string message, float duration, Sprite icon = null)
    {
        _message = message;
        _messageText.text = _message;
        _duration = duration;
        _icon.sprite = icon;
        StartCoroutine(Hide());
    }

    private void OnEnable()
    {
        _canvasGroup.alpha = 1.0f;
        transform.SetAsLastSibling();
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(_duration);
        
        float timer = 0;
        while (timer < _fadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        if (Application.isPlaying)
            NotificationTimedOut?.Invoke(this);
    }

    private void OnDestroy()
    {
        NotificationTimedOut = null;
        StopAllCoroutines();
    }
}
