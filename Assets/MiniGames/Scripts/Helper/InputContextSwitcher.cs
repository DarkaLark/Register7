using UnityEngine;
using UnityEngine.InputSystem;

public class InputContextSwitcher : MonoBehaviour
{
    [SerializeField] private GameStateGameEvent _onGameStateChanged;
    [SerializeField] private ListOfMiniGames.MiniGame _miniGame;
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
        if (state == GameState.Playing)
            SwitchToPlayerInput();
        else
            SwitchToMiniGameInput();
    }

    private void SwitchToMiniGameInput()
    {
        _playerInput.SwitchCurrentActionMap(_miniGame.ToString());
    }

    private void SwitchToPlayerInput()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }
}
