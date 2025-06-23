using System.Collections;
using UnityEngine;

public class PokerHandler : MonoBehaviour
{
    [Header("Event Actions")]
    [SerializeField] private Vector2GameEvent _onMousePointerPosition;
    [SerializeField] private Vector2GameEvent _onGamepadPointerPosition;
    [SerializeField] private GameEvent _onPointerClick;

    [Header("Cursor")]
    [Space(5)]
    [SerializeField] private RectTransform _cursorVisual;
    private Vector3 _cursorClickAnimation = new(-10, 0, 0);
    private Vector3 _cursorAnimationOffset = Vector3.zero;
    [SerializeField] private float _cursorSpeed = 1000f;
    private Vector2 _cursorOffset = new(75, -360);
    private Vector2 _cursorPosition;

    [Header("Splat Sound")]
    [Space(5)]
    private AudioSource _audio;
    [SerializeField] private AudioClip _splat;

    [Header("Player Input Device")]
    private bool _isMouse = false;

    void Awake()
    {
        _audio = FindFirstObjectByType<AudioSource>();
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
        _isMouse = true;

        VisualizeCursor();
    }

    private void OnGamepadPointerMove(Vector2 input)
    {
        _cursorPosition += _cursorSpeed * Time.deltaTime * input;
        _isMouse = false;

        VisualizeCursor();
    }

    private void VisualizeCursor()
    {
        _cursorPosition = ClampToScreen(_cursorPosition);

        if (_cursorVisual != null)
        {
            _cursorVisual.position = _cursorPosition + _cursorOffset + (Vector2)_cursorAnimationOffset;
        }
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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)
            && hit.collider.TryGetComponent(out Bubble bubble))
        {
            bubble.Pop();
        }
        else if (Physics.Raycast(ray, out hit)
            && hit.collider.gameObject.name == "Pizza")
        {
            if (_audio != null)
                PlayAudio();
        }

        if (_isMouse == true)
            StartCoroutine(MousePopAnimation());
        else
            StartCoroutine(GamepadPopAnimation());
    }

    #region OnPointerClick func's
    private void PlayAudio()
    {
        _audio.pitch = Random.Range(.95f, 1.05f);
        _audio.PlayOneShot(_splat);
    }

    private IEnumerator MousePopAnimation()
    {
        _cursorVisual.position += _cursorClickAnimation;

        yield return new WaitForSeconds(0.1f);
        _cursorVisual.position -= _cursorClickAnimation;

    }
    private IEnumerator GamepadPopAnimation()
    {
        _cursorAnimationOffset = _cursorClickAnimation;

        yield return new WaitForSeconds(0.1f);

        _cursorAnimationOffset = Vector3.zero;
    }
    #endregion
}