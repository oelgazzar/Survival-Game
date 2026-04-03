using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] TMP_Text _interactionInfoText;
    [SerializeField] float _raycastDistance = 10f;
    [SerializeField] float _raycastRadius = .1f;
    [SerializeField] LayerMask _raycastLayerMask;

    public Interactable Target;


    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused)
        {
            return;
        }

        var ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.SphereCast(ray, _raycastRadius, out var hit, _raycastDistance, _raycastLayerMask))
        {
            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                Target = interactable;
                interactable.Hover(true);
                return;
            }
        }
        if (Target != null)
            Target.Hover(false);
        Target = null;
    }
}