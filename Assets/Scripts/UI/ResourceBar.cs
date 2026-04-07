using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] TMP_Text _resourceNameText;


    public void UpdateValue(float value)
    {
        _fillImage.fillAmount = value;
    }

    public void UpdateName(string name)
    {
        _resourceNameText.text = name;
    }
}
