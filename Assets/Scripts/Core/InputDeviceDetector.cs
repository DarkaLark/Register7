using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputDeviceDetector : MonoBehaviour
{
    public InputControlScheme InputControlScheme { get; private set; }
    [Header("Player Input")]
    [Space(5)]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Event")]
    [Space(5)]
    [SerializeField] private GameEvent _onPlayerInputChanged;
    [SerializeField] private GameEvent _onInputChanged;
    private void OnEnable()
    {
        _playerInput.onActionTriggered += OnAnyButtonPress;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnAnyButtonPress;
    }

    private void OnAnyButtonPress(InputAction.CallbackContext ctx)
    {
        switch (ctx.control.device)
        {
            case Gamepad:
            {
                if (InputControlScheme != InputControlScheme.Gamepad)
                {
                    InputControlScheme = InputControlScheme.Gamepad;
                    
                    _onInputChanged.Raise();
                    print("Switching to Gamepad");
                }

                break;
            }
            case Keyboard or Mouse:
            {
                if (InputControlScheme != InputControlScheme.KeyboardMouse)
                {
                    InputControlScheme = InputControlScheme.KeyboardMouse;
                    
                    _onInputChanged.Raise();
                    print("Switching to Mouse and Keyboard");
                }

                break;
            }
        }
    }
}