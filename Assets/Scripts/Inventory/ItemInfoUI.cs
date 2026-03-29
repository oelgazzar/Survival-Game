using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _functionalityText;

    ItemInfo _info;

    public void SetItemInfo(ItemInfo info)
    {
        _info = info;
        _nameText.text = _info.Name;
        _descriptionText.text = _info.Description;
        _functionalityText.text = _info.Functionality;
    }

}
