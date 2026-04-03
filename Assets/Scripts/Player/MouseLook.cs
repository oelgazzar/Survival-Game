using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class MouseLook : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] float _sensitivity;
    [SerializeField] float _minRotationX;
    [SerializeField] float _maxRotationX;

    PlayerInputManager _inputManager;

    private float _rotationX;

    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused) return;

        var mouseInput = _inputManager.MouseMoveAction.ReadValue<Vector2>();

        _rotationX -= mouseInput.y * _sensitivity * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);
        _cam.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        var rotationY = mouseInput.x;
        transform.Rotate(Vector3.up, rotationY);
    }
}
