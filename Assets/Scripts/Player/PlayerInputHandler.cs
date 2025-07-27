using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onInteractPress;
    [SerializeField] private Vector2GameEvent _onMoveInput;
    [SerializeField] private BoolGameEvent _onSprintDown;

    [SerializeField] private DialogueStateGameEvent _onDialogueStateChanged;
    
    private bool _canInteract = true;

    public Vector2 MoveInput { get; private set; }
    private bool _isSprinting = false;
    private bool _canAdvanceDialogue;

    private PlayerInput _playerInput;
    private PlayerItemCarrier _playerItemCarrier;

    private InputAction _playerInteract;
    private InputAction _playerMove;
    private InputAction _playerSprint;

    void Awake()
    {
        TryGetComponent(out _playerItemCarrier);
        TryGetComponent(out _playerInput);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerInteract = _playerInput.actions["Interact"];
        _playerMove = _playerInput.actions["Move"];
        _playerSprint = _playerInput.actions["Sprint"];

        _onDialogueStateChanged.Register(HandleDialogueStateChange);
    }

    void OnDestroy()
    {
        _onDialogueStateChanged.Unregister(HandleDialogueStateChange);
    }

    void OnEnable()
    {
        _playerInteract.performed += OnInteract;

        _playerMove.performed += OnMove;
        _playerMove.canceled += OnMove;

        _playerSprint.performed += OnSprint;
        _playerSprint.canceled += OnSprint;
    }

    void OnDisable()
    {
        _playerInteract.performed -= OnInteract;

        _playerMove.performed -= OnMove;
        _playerMove.canceled -= OnMove;

        _playerSprint.performed -= OnSprint;
        _playerSprint.canceled -= OnSprint;

        MoveInput = Vector2.zero;
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_playerItemCarrier != null && _playerItemCarrier.HasItem)
        {
            _playerItemCarrier.DropItem();
            return;
        }
            
        if (_canInteract)
            _onInteractPress.Raise();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        MoveInput = input;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValue<float>() > 0.1f;
        _onSprintDown.Raise(_isSprinting);
    }

    private void HandleDialogueStateChange(DialogueState newState)
    {
        _canInteract = newState == DialogueState.None;
        _canAdvanceDialogue = newState == DialogueState.Listening;
    }
}