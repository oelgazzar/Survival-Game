using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] TMP_Text _interactionInfoText;
    [SerializeField] float _raycastDistance = 10f;
    [SerializeField] float _raycastRadius = .1f;

    public static InteractionManager Instance;

    public Interactable Target;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        var ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.SphereCast(ray, _raycastRadius, out var hit, _raycastDistance))
        {
            var interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null && interactable.PlayerInRange)
            {
                _interactionInfoText.text = interactable.Name;
                _interactionInfoText.gameObject.SetActive(true);
                Target = interactable;
                return;
            }
        }
        _interactionInfoText.gameObject.SetActive(false);
        Target = null;
    }
}