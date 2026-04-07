using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISaveable
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpHeight;
    [SerializeField] float _gravity = -9.8f;
    float velocityY;

    PlayerInputManager _inputManager;

    public string SaveID { get => "player"; }

    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Paused) return;

        var moveInput = _inputManager.MoveAction.ReadValue<Vector2>();
        var direction = moveInput.x * transform.right + moveInput.y * transform.forward;
        var velocity = direction * _moveSpeed;

        velocityY += _gravity * Time.deltaTime;

        if (velocityY < 0 && _characterController.isGrounded)
        {
            velocityY = -2;
        }

        if (_inputManager.JumpAction.WasPressedThisFrame() && _characterController.isGrounded)
        {
            velocityY = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }


        velocity.y = velocityY;
        _characterController.Move(velocity * Time.deltaTime);
    }

    public void Load(string state)
    {
        var playerData = JsonUtility.FromJson<SaveablePlayerData>(state);
        transform.SetPositionAndRotation(playerData.Position, playerData.Rotation);
    }

    public string Save()
    {
        return JsonUtility.ToJson(new SaveablePlayerData()
        {
            Position = transform.position,
            Rotation = transform.rotation
        });
    }
}
