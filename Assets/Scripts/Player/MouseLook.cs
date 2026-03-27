using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] InputAction _mouseMoveAction;
    [SerializeField] Camera _cam;
    [SerializeField] float _sensitivity;
    [SerializeField] float _minRotationX;
    [SerializeField] float _maxRotationX;

    private float _rotationX;

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused) return;

        var mouseInput = _mouseMoveAction.ReadValue<Vector2>();

        _rotationX -= mouseInput.y * _sensitivity * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);
        _cam.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        var rotationY = mouseInput.x;
        transform.Rotate(Vector3.up, rotationY);
    }

    private void OnEnable()
    {
        _mouseMoveAction.Enable();
    }

    private void OnDisable()
    {
        _mouseMoveAction.Disable();
    }
}
