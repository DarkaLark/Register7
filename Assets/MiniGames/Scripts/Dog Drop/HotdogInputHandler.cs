using UnityEngine;
using UnityEngine.InputSystem;

public class HotdogInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onDropHotdogPress;

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
    }

    void OnDisable()
    {
        _drop.performed -= OnDrop;
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        _onDropHotdogPress.Raise();
    }
}