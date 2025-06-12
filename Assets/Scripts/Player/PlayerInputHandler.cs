using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onInteractPress;
    [SerializeField] private GameEvent _onAdvanceDialoguePress;
    [SerializeField] private PlayerMovementGameEvent _onMoveInput;

    [SerializeField] private DialogueStateGameEvent _onDialogueStateChanged;
    private bool _canInteract = true;
    private bool _canAdvanceDialogue = false;

    public Vector2 MoveInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _playerInteract;
    private InputAction _playerMove;
    private InputAction _playerAdvanceDialogue;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInteract = _playerInput.actions["Interact"];
        _playerMove = _playerInput.actions["Move"];
        _playerAdvanceDialogue = _playerInput.actions["Advance Dialogue"];

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
    }

    void OnDisable()
    {
        _playerInteract.performed -= OnInteract;

        _playerMove.performed -= OnMove;
        _playerMove.canceled -= OnMove;

        _playerAdvanceDialogue.performed -= OnAdvanceDialogue;

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

    private void HandleDialogueStateChange(DialogueState newState)
    {
        _canInteract = newState == DialogueState.None;
        _canAdvanceDialogue = newState == DialogueState.Listening;
    }
}