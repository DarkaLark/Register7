using UnityEngine;
using UnityEngine.InputSystem;

public class InputContextSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputAsset;

    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = FindFirstObjectByType<PlayerInput>();
        _onGameStateChanged.Register(Switch);
    }

    void OnDestroy()
    {
        _onGameStateChanged.Unregister(Switch);
    }

    private void Switch(GameState state)
    {
        Debug.Log("Switching control settings to: " + state);
        if (state == GameState.Playing)
            SwitchToPlayerInput();
        else
            SwitchToMiniGameInput();
    }

    private void SwitchToMiniGameInput()
    {
        _playerInput.SwitchCurrentActionMap("Mini Game");
    }

    private void SwitchToPlayerInput()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }
}
