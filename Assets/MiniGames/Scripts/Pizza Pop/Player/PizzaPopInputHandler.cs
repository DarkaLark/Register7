using UnityEngine;
using UnityEngine.InputSystem;

public class PizzaPopInputHandler : MonoBehaviour
{
    [SerializeField] private Vector2GameEvent _onMousePointerPosition;
    [SerializeField] private Vector2GameEvent _onGamepadPointerPosition;

    [SerializeField] private GameEvent _onPointerClick;

    private PlayerInput _playerInput;

    private InputAction _mousePointerPosition;
    private InputAction _gamepadPointerPosition;
    private InputAction _pointerClick;

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _playerInput = FindFirstObjectByType<PlayerInput>();

        _mousePointerPosition = _playerInput.actions["MousePointerPosition"];
        _gamepadPointerPosition = _playerInput.actions["GamepadPointerPosition"];

        _pointerClick = _playerInput.actions["PointerClick"];
    }

    void OnEnable()
    {
        _mousePointerPosition.performed += OnMousePointerPosition;

        _pointerClick.performed += OnPointerClick;
    }

    void OnDisable()
    {
        _mousePointerPosition.performed -= OnMousePointerPosition;

        _pointerClick.performed -= OnPointerClick;
    }

    void Update()
    {
        if (_gamepadPointerPosition != null && _playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 input = _gamepadPointerPosition.ReadValue<Vector2>();
            _onGamepadPointerPosition.Raise(input);
        }
    }

    private void OnMousePointerPosition(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _onMousePointerPosition.Raise(input);
    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
        _onPointerClick.Raise();
    }
}