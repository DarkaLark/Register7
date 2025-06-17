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

    [SerializeField] private GameObject _bubblePrefab;

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
        Ray ray = Camera.main.ScreenPointToRay(_cursorPosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == _bubblePrefab)
        {
            Debug.Log("Hit bubble prefab");

            Destroy(_bubblePrefab.gameObject);
        }
    }
}