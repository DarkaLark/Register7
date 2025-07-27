
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class CharacterMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _sprintSpeed = 5f;
    [SerializeField] private float _sprintDuration = 1f;
    [SerializeField] private float _sprintCooldown = 3f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed = 60f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private Animator _animator;

    [Space(5)]
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private DialogueStateGameEvent _onDialogueChanged;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;
    [SerializeField] private BoolGameEvent _onSprintDown;

    [Space(5)]
    [SerializeField] private Camera _camera;

    private Vector3 _inputVector = new(0f, 0f, 0f);
    private bool _canMove = true;

    private float _sprintTime = 0f;
    private float _sprintCooldownTime = 0f;
    private bool _isSprinting = false;

    void OnEnable()
    {
        _onDialogueChanged.Register(HandleDialogueStateChange);
        _onGameStateChanged.Register(HandleGameStateChange);
        _onSprintDown.Register(TrySprinting);
    }

    void OnDisable()
    {
        _onDialogueChanged.Unregister(HandleDialogueStateChange);
        _onGameStateChanged.Unregister(HandleGameStateChange);
        _onSprintDown.Unregister(TrySprinting);

        _inputVector = Vector3.zero;
    }

    private void HandleDialogueStateChange(DialogueState state)
    {
        _canMove = state == DialogueState.None;
    }

    private void HandleGameStateChange(GameState state)
    {
        _canMove = state == GameState.Playing;
    }

    void Update()
    {
        if (!_canMove) return;

        ReduceSprintCd();

        TryJumping();

        _inputVector = GetMovementDirectionRelativeToCamera();
    }

    #region Update() funcs
    private void TryJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.2f))
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        _rb.AddForce(0f, _jumpForce, 0f, ForceMode.Impulse);
    }

    private void TrySprinting(bool sprintDown)
    {
        if (_sprintCooldownTime <= 0f && sprintDown)
        {
            if (_sprintTime < _sprintDuration)
            {
                _isSprinting = true;
                _sprintTime += Time.deltaTime;
            }
            else
            {
                _isSprinting = false;
                _sprintCooldownTime = _sprintCooldown;
                _sprintTime = 0f;
            }
        }
        else
        {
            _isSprinting = false;
        }
    }

    private void ReduceSprintCd()
    {
        if (!_canMove) return;

        if (_sprintCooldownTime > 0f)
        {
            _sprintCooldownTime -= Time.deltaTime;
        }
    }

    private Vector3 GetMovementDirectionRelativeToCamera()
    {
        Vector2 _input = _inputHandler.MoveInput;
        if (_input.sqrMagnitude > 0.01f)
        {
            Vector3 camForward = _camera.transform.forward;
            Vector3 camRight = _camera.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            return camForward * _input.y + camRight * _input.x;
        }
        else
            return Vector3.zero;
    }

    #endregion

    private void FixedUpdate()
    {
        Vector3 targetVelocity = GetTargetVelocity();
        targetVelocity = RotateCharacterOnInput(targetVelocity);

        _rb.linearVelocity = _canMove ? targetVelocity : Vector3.zero;

        AnimatePlayer(targetVelocity);
    }

    #region FixedUpdate() funcs
    private Vector3 GetTargetVelocity()
    {
        var currentSpeed = _isSprinting ? _sprintSpeed : _speed;

        var targetVelocity = _inputVector * currentSpeed;
        targetVelocity.y = _rb.linearVelocity.y;

        return targetVelocity;
    }

    private Vector3 RotateCharacterOnInput(Vector3 targetVelocity)
    {
        Vector3 planarVelocity = Vector3.ProjectOnPlane(targetVelocity, Vector3.up);
        if (planarVelocity.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(planarVelocity);
            Quaternion smoothRotation = Quaternion.RotateTowards(_rb.rotation, targetRotation, _rotationSpeed);
            _rb.MoveRotation(smoothRotation);
        }

        return targetVelocity;
    }
    
    private void AnimatePlayer(Vector3 targetVelocity)
    {
        float animateSpeed = _canMove ? targetVelocity.magnitude : 0f;
        _animator.SetFloat("Speed", animateSpeed);
    }
    #endregion

}
