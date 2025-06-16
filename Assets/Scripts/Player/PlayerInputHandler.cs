using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public enum InputType
    {
        KeyboardAndMouse,
        XBoxController
    }

    [SerializeField] private GameEvent _onInteractPress;
    [SerializeField] private GameEvent _onAdvanceDialoguePress;
    [SerializeField] private Vector2GameEvent _onMoveInput;
    [SerializeField] private BoolGameEvent _onSprintDown;

    [SerializeField] private DialogueStateGameEvent _onDialogueStateChanged;
    private bool _canInteract = true;
    private bool _canAdvanceDialogue = false;

    public Vector2 MoveInput { get; private set; }
    private bool _isSprinting = false;

    private PlayerInput _playerInput;

    private InputAction _playerInteract;
    private InputAction _playerMove;
    private InputAction _playerAdvanceDialogue;
    private InputAction _playerSprint;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerInteract = _playerInput.actions["Interact"];
        _playerMove = _playerInput.actions["Move"];
        _playerAdvanceDialogue = _playerInput.actions["Advance Dialogue"];
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

        _playerAdvanceDialogue.performed += OnAdvanceDialogue;

        _playerSprint.performed += OnSprint;
        _playerSprint.canceled += OnSprint;
    }

    void OnDisable()
    {
        _playerInteract.performed -= OnInteract;

        _playerMove.performed -= OnMove;
        _playerMove.canceled -= OnMove;

        _playerAdvanceDialogue.performed -= OnAdvanceDialogue;

        _playerSprint.performed -= OnSprint;
        _playerSprint.canceled -= OnSprint;

        MoveInput = Vector2.zero;
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_canInteract)
            _onInteractPress.Raise();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        MoveInput = input;
    }

    private void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (_canAdvanceDialogue)
            _onAdvanceDialoguePress.Raise();
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