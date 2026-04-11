using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] DialogData _dialogData;
    
    Animator _animator;

    bool _isEyeFollowingPlayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Hover(bool value)
    {
        UIManager.Instance.UpdateInteractionInfoText(value ? "Talk" : "");

        if (value == false)
        {
            _animator.SetBool("IsTalking", false);
        }

        _isEyeFollowingPlayer = value;
    }

    private void Update()
    {
        if (_isEyeFollowingPlayer)
        {
            var camPos = Camera.main.transform.position;
            var lookDir = (camPos - transform.position).normalized;
            var targetRotY = Quaternion.LookRotation(lookDir).eulerAngles.y;
            var rotationY = Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetRotY, Time.deltaTime * 5);
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    public override bool Interact()
    {
        _animator.SetBool("IsTalking", true);
        DialogManager.Instance.StartDialog(_dialogData);
        return true;
    }
}
