using UnityEngine;
using UnityEngine.InputSystem;

public class HotdogInputHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent _onDropHotdogPress;
    [SerializeField] private GameEvent _onCountdownFinished;

    private bool _canInput = false;

    private PlayerInput _playerInput;
    private InputAction _drop;


    void Awake()
    {
        _playerInput = FindFirstObjectByType<PlayerInput>();
        _drop = _playerInput.actions["Drop"];
    }

    void OnEnable()
    {
        _drop.performed += OnDrop;
        _onCountdownFinished.Register(OnCountdownFinished);
    }

    void OnDisable()
    {
        _drop.performed -= OnDrop;
        _onCountdownFinished.Unregister(OnCountdownFinished);
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        if (!_canInput)
            return;
        _onDropHotdogPress.Raise();
    }

    private void OnCountdownFinished() =>_canInput = true;
}