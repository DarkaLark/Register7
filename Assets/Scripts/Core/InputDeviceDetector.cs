using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceDetector : MonoBehaviour
{
    public InputControlScheme InputControlScheme { get; private set; }
    [SerializeField] private PlayerInput _playerInput;

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
                    print("Switching to Gamepad");
                }

                break;
            }
            case Keyboard or Mouse:
            {
                if (InputControlScheme != InputControlScheme.KeyboardMouse)
                {
                    InputControlScheme = InputControlScheme.KeyboardMouse;
                    print("Switching to Mouse and Keyboard");
                }

                break;
            }
        }
    }
}