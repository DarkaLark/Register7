
using UnityEngine;

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
    [SerializeField] private DialogueStateGameEvent _onDialogueChanged;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    
    private Vector3 _inputVector = new Vector3(0f, 0f, 0f);
    private bool _canMove = true;

    private float _sprintTime = 0f;
    private float _sprintCooldownTime = 0f;
    private bool _isSprinting = false;


    void OnEnable()
    {
        _onDialogueChanged.Register(HandleDialogueStateChange);
        _onGameStateChanged.Register(HandleGameStateChange);
    }

    void OnDisable()
    {
        _onDialogueChanged.Unregister(HandleDialogueStateChange);
        _onGameStateChanged.Unregister(HandleGameStateChange);
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

        ReduceSprintCD();

        TrySprinting();
        TryJumping();
 
        _inputVector = GetNormalizedInput();

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
        // Apply jump force
        _rb.AddForce(0f, _jumpForce, 0f, ForceMode.Impulse);
    }

    private void TrySprinting()
    {
        // If we're not in cooldown and the player is pressing shift and there's no sprinting time left
        if (_sprintCooldownTime <= 0f && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (_sprintTime < _sprintDuration)
            {
                // Sprinting allowed
                _isSprinting = true;
                _sprintTime += Time.deltaTime;
            }
            else
            {
                // Sprint time is up, start cooldown
                _isSprinting = false;
                _sprintCooldownTime = _sprintCooldown;
                _sprintTime = 0f;
            }
        }
        else
        {
            // If not sprinting, use normal speed
            _isSprinting = false;
        }
    }

    private void ReduceSprintCD()
    {
        if (!_canMove) return;

        if (_sprintCooldownTime > 0f)
        {
            _sprintCooldownTime -= Time.deltaTime;
        }
    }

    private Vector3 GetNormalizedInput()
    {
        Vector3 normalizedMovement;

        if (_canMove)
            normalizedMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        else
            normalizedMovement = Vector3.zero;

        return normalizedMovement;
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
    private void AnimatePlayer(Vector3 targetVelocity)
    {
        float animateSpeed = _canMove ? targetVelocity.magnitude : 0f;
        _animator.SetFloat("Speed", animateSpeed);
    }

    private Vector3 GetTargetVelocity()
    {
        float currentSpeed = _isSprinting ? _sprintSpeed : _speed;

        Vector3 targetVelocity = _inputVector * currentSpeed;
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
    #endregion

}
