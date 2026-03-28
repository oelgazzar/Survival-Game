using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _percentageText;
    [SerializeField] PlayerStatusData _statusData;

    public void Start()
    {
        _fillImage.sprite = _statusData.Color;
        _icon.sprite = _statusData.Icon;
        UpdateValue(_statusData.MaxValue);
    }

    public void UpdateValue(float value)
    {
        value = Mathf.Clamp(value, 0, _statusData.MaxValue);
        _fillImage.fillAmount = value / _statusData.MaxValue;
        _percentageText.text = $"{value:F0} / {_statusData.MaxValue}";
    }

    void OnStatusChanged(PlayerStatus playerStatus)
    {
        if (playerStatus.StatusData == _statusData)
        {
            UpdateValue(playerStatus.CurrentValue);
        }
    }

    private void OnEnable()
    {
        PlayerStatusManager.StatusChanged += OnStatusChanged;
    }

    private void OnDisable()
    {
        PlayerStatusManager.StatusChanged -= OnStatusChanged;
    }

    [ContextMenu("Test Update Value")]
    void TestUpdateValue()
    {
        UpdateValue(_statusData.MaxValue / 2);
    }
}
