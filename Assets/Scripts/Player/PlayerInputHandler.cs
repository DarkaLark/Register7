using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onEKeyPress;
    [SerializeField] private PlayerMovementGameEvent _onMoveInput;

    public Vector2 MoveInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _playerInteract;
    private InputAction _playerMove;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInteract = _playerInput.actions["Interact"];
        _playerMove = _playerInput.actions["Move"];
    }

    void OnEnable()
    {
        _playerInteract.performed += OnInteract;

        _playerMove.performed += OnMove;
        _playerMove.canceled += OnMove;
    }

    void OnDisable()
    {
        _playerInteract.performed -= OnInteract;

        _playerMove.performed -= OnMove;
        _playerMove.canceled -= OnMove;

        MoveInput = Vector2.zero;
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        _onEKeyPress.Raise();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        MoveInput = input;
    }
}