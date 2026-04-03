using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _hoverCursorIcon;

    public virtual void Hover(bool value)
    {
        if (value == true)
        {
            UIManager.Instance.UpdateInteractionInfoText(_name);
            UIManager.Instance.UpdateScreenCenterIcon(
                _hoverCursorIcon != null ?
                _hoverCursorIcon : UIManager.Instance.DefaultScreenCenterIcon);
        } else
        {
            UIManager.Instance.UpdateInteractionInfoText(null);
            UIManager.Instance.UpdateScreenCenterIcon(UIManager.Instance.DefaultScreenCenterIcon);
        }
    }

    public virtual bool Interact() { return false; }
}
