using UnityEngine;
using UnityEngine.InputSystem;

public class PokerHandler : MonoBehaviour
{
    [Header("Event Actions")]
    [SerializeField] private Vector2GameEvent _onMousePointerPosition;
    [SerializeField] private Vector2GameEvent _onGamepadPointerPosition;
    [SerializeField] private GameEvent _onPointerClick;

    [Header("Cursor")]
    [Space(5)]
    [SerializeField] private RectTransform _cursorVisual;
    [SerializeField] private float _cursorSpeed = 1000f;
    private Vector2 _cursorPosition;

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            Debug.Log("Player is using Gamepad");
        }
        else if (Mouse.current != null && (Mouse.current.delta.ReadValue() != Vector2.zero || Mouse.current.leftButton.wasPressedThisFrame))
        {
            Debug.Log("Player is using Keyboard and Mouse");
        }
    }

    void OnEnable()
    {
        _onMousePointerPosition.Register(OnMousePointerMove);
        _onGamepadPointerPosition.Register(OnGamepadPointerMove);

        _onPointerClick.Register(OnPointerClick);
    }

    void OnDisable()
    {
        _onMousePointerPosition.Unregister(OnMousePointerMove);
        _onGamepadPointerPosition.Unregister(OnGamepadPointerMove);
        _onPointerClick.Unregister(OnPointerClick);
    }

    private void OnMousePointerMove(Vector2 input)
    {
        _cursorPosition = input;

        VisualizeCursor();
    }

    private void OnGamepadPointerMove(Vector2 input)
    {
        _cursorPosition += _cursorSpeed * Time.deltaTime * input;

        VisualizeCursor();
    }

    private void VisualizeCursor()
    {
        _cursorPosition = ClampToScreen(_cursorPosition);

        if (_cursorVisual != null)
            _cursorVisual.position = _cursorPosition;
    }

    private Vector2 ClampToScreen(Vector2 pos)
    {
        float x = Mathf.Clamp(pos.x, 0, Screen.width);
        float y = Mathf.Clamp(pos.y, 0, Screen.height);
        return new Vector2(x, y);
    }

    private void OnPointerClick()
    {
        Debug.Log($"Clicker was pressed at : {_cursorPosition}");
    }
}